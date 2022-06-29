using Membership.Core.Abstractions;
using Membership.Core.Consts;
using Membership.Core.Entities.Commons;
using Membership.Core.Repositories.Commons;
using Microsoft.AspNetCore.Http;

namespace Membership.Infrastructure.FileManagement;

internal sealed class BufferedFileUploadLocalService : IBufferedFileUploadService
{
    private readonly IFileAttachmentRepository _attachmentRepository;
    private readonly IClock _clock;
    
    public BufferedFileUploadLocalService(IFileAttachmentRepository attachmentRepository, IClock clock)
    {
        _attachmentRepository = attachmentRepository;
        _clock = clock;
    }
    
    public async Task<Guid?> UploadFile(IFormFile file, string filePath, FileType type)
    {
        if (file.Length > 0)
        {
            filePath ??= Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles"));

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            var id = Guid.NewGuid();
            var newFileName = $"{id}___{file.FileName}";
            var extension = Path.GetExtension(file.FileName);
            var newPath = Path.Combine(filePath, newFileName);

            if (File.Exists(newPath))
            {
                throw new Exception("File Copy Failed");
            }

            using (var fileStream = new FileStream(newPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            var fileAttachment = FileAttachment.Create(id, type, file.FileName, newFileName,
                extension, extension, newPath, file.Length, _clock.Current());

            await _attachmentRepository.AddAsync(fileAttachment);

            return id;
        }
        else
        {
            return null;
        }
    }

    public async Task<MemoryStream> Download(Guid fileGuid, string filePath)
    {
        var file = await _attachmentRepository.GetByIdAsync(fileGuid);

        if (file is null)
        {
            throw new FileNotFoundException();
        }

        var memoryStream = new MemoryStream();

        if (!File.Exists(Path.Combine(filePath, file.FileName)))
        {
            throw new FileNotFoundException();
        }
        
        using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Open))
        {
            await fileStream.CopyToAsync(memoryStream);
        }
        
        memoryStream.Position = 0;

        return new BufferedFileUploadDto
        { 
            Memory = memoryStream,
            FileType = file.FileType,
            FileName = file.FileName
        };
    }
}