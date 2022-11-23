using Azure.Storage.Blobs;
using Membership.Application.Abstractions;
using Membership.Application.Queries.Verifications;
using Membership.Core.Consts;
using Membership.Core.Repositories.Commons;
using Membership.Infrastructure.OCR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Membership.Infrastructure.DAL.Handlers.Verifications;


internal sealed class AutoVerificationHandler : IQueryHandler<AutoVerification, bool>
{
    private readonly MembershipDbContext _dbContext;
    private readonly IOcrVerifyService _ocrVerifyService;
    private readonly IFileAttachmentRepository _fileAttachmentRepository;
    private readonly FileUploadOptions _fileUploadOptions;
    
    public AutoVerificationHandler(MembershipDbContext dbContext, IOcrVerifyService ocrVerifyService, 
        IFileAttachmentRepository fileAttachmentRepository, IOptions<FileUploadOptions> fileUploadOptions)
    {
        _dbContext = dbContext;
        _ocrVerifyService = ocrVerifyService;
        _fileUploadOptions = fileUploadOptions.Value;
        _fileAttachmentRepository = fileAttachmentRepository;
    }

    public async Task<bool> HandleAsync(AutoVerification query)
    {
        string connectionString = _fileUploadOptions.StorageConnection;
        
        var uploadFilePath = @"C:\DownloadFile\";

        var saveLocation = $"{uploadFilePath}{query.ProcessDate.ToString("yyyyMMdd")}";

        if (!Directory.Exists(saveLocation))
        {
            Directory.CreateDirectory(saveLocation);
        }

        var result = await _dbContext.FileAttachments
            .Where(x => x.CreatedAt.Year == query.ProcessDate.Year &&
                        x.CreatedAt.Month == query.ProcessDate.Month &&
                        x.CreatedAt.Day == query.ProcessDate.Day &&
                        (x.Type == FileType.PassportFirst || x.Type == FileType.PassportLast))
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

        foreach (var item in result)
        {
            try
            {  
                BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(item.FilePath);

                BlobClient blobClient = containerClient.GetBlobClient(item.SavedFileName);
                
                using var memoryStream = new MemoryStream();
                await blobClient.DownloadToAsync(memoryStream);
                memoryStream.Position = 0;

                var fileName = $"{saveLocation}\\{item.CreatedAt.ToString("HHmm")}___{item.SavedFileName}";
                
                if (File.Exists(fileName))
                {
                    throw new Exception("File Copy Failed");
                }

                using (var fileStream = new FileStream(fileName, FileMode.Create))
                {
                    await memoryStream.CopyToAsync(fileStream);
                }

            }
            catch (DirectoryNotFoundException ex)
            {
                // Let the user know that the directory does not exist
                Console.WriteLine($"Directory not found: {ex.Message}");
            }
        }

        return true;
    }
}