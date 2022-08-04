using Azure.Storage.Blobs;
using Membership.Application.DTO.Commons;
using Membership.Core.Abstractions;
using Membership.Core.Consts;
using Membership.Core.Entities.Commons;
using Membership.Core.Repositories.Commons;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Membership.Infrastructure.FileManagement;

public class AzureStorageBlobsFileUploadService : IBufferedFileUploadService
{
    private readonly IFileAttachmentRepository _attachmentRepository;
    private readonly IClock _clock;
    private readonly FileUploadOptions _fileUploadOptions;
    
    public AzureStorageBlobsFileUploadService(IFileAttachmentRepository attachmentRepository, 
        IClock clock, IOptions<FileUploadOptions> fileUploadOptions)
    {
        _attachmentRepository = attachmentRepository;
        _clock = clock;
        _fileUploadOptions = fileUploadOptions.Value;
    }
    public async Task<Guid?> UploadFile(IFormFile file, string filePath, FileType type)
    {
        if (file.Length > 0)
        {
            var id = Guid.NewGuid();
            var newFileName = $"{id}___{file.FileName}";
            var extension = Path.GetExtension(file.FileName);
            var fileType = extension.Replace(".", string.Empty);
            var folderYear = _clock.Current().Year.ToString();

            string connectionString = _fileUploadOptions.StorageConnection;
            string containerName = $"{_fileUploadOptions.FilePath}-{type.ToString().ToLower()}-{folderYear}";
            
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            bool isExist =  containerClient.Exists();
            if (!isExist)
            {
                containerClient.Create();
            }

            BlobClient blobClient = containerClient.GetBlobClient(newFileName);

            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    ms.Position = 0;
                    await blobClient.UploadAsync(ms, true);
                }
                
                var fileAttachment = FileAttachment.Create(id, type, file.FileName, newFileName,
                    extension, $"application/{fileType}", containerName, file.Length, _clock.Current());

                await _attachmentRepository.AddAsync(fileAttachment);

            }

            return id;
        }
        else
        {
            return null;
        }
    }

    public async Task<BufferedFileUploadDto> Download(Guid fileGuid, string filePath)
    {
        var file = await _attachmentRepository.GetByIdAsync(fileGuid);

        if (file is null)
        {
            throw new FileNotFoundException();
        }

        string connectionString = _fileUploadOptions.StorageConnection;
        string containerName = file.FilePath;
            
        BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        BlobClient blobClient = containerClient.GetBlobClient(file.SavedFileName);
        
        using var stream = new MemoryStream();
        await blobClient.DownloadToAsync(stream);
        stream.Position = 0;

        return new BufferedFileUploadDto
        { 
            File =  stream.ToArray(),
            FileType = file.FileType,
            FileName = file.ActualFileName
        };
    }
}