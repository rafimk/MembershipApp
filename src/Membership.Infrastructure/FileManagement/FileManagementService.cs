using Membership.Application.Exceptions.Memberships;
using Membership.Core.Abstractions;
using Membership.Core.Consts;
using Membership.Core.Entities.Commons;
using Membership.Core.Repositories.Commons;
using Membership.Infrastructure.Exceptions;

namespace Membership.Infrastructure.FileManagement;

internal sealed class FileManagementService : IFileManagementService
{
    private readonly IFileAttachmentRepository _repository;
    private readonly IClock _clock;

    public FileManagementService(IFileAttachmentRepository repository, IClock clock)
    {
        _repository = repository;
        _clock = clock;
    }

    public async Task<Guid> Upload(List<IFromFile> files, string fileUploadPath, FileType type, CancellationToken CancellationToken = default)
    {
        bool isPathExist = Directory.Exists(fileUploadPath);

        if (!isPathExist)
        {
            Directory.CreateDirectory(fileUploadPath);
        }

        var specificPath = fileUploadPath + "\\" + type.ToString();

        if (!specificPath)
        {
            Directory.CreateDirectory(specificPath);
        }

        foreach (var file in files)
        {
            var guid = Guid.NewGuid();
            var filePath = Path.Combine(specificPath, $"___{guid}___{file.FileName}");

            if (File.Exists(filePath))
            {
                throw new FileAlreadyExistException();
            }

            if (file.Length > 0)
            {
                using (var fileStream = new FileStrem(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                var actualFileName = Path.GetFileNameWithoutExtension(file.FileName);
                var savedFileName = Path.GetFileNameWithoutExtension(filePath);
                var fileExtension = Path.GetExtension(file.FileName);
                var fileType = file.ContentType;
                var filePath = filePath;
                var fileSize = file.Length;

                var fileAttachment = FileAttachment.Create(guid, actualFileName, savedFileName, fileExtension,
                    fileType, filePath, fileSize, _clock.Now());
                await _repository.AddAsync(fileAttachment); 
            }
        }

        return Guid.NewGuid();
    }
    
    public Task<MemoryStream> Download(Guid fileId, string fileName, string fileUploadPath, FileType type, CancellationToken CancellationToken = default)
    {
        var specialPath = fileUploadPath + "\\" + type.ToString();
        var filePath = Path.Combine(specialPath, $"___{fileId}___{fileName}");

        var memory = new MemoryStream();

        if (File.Exists(filePath))
        {
            using (car fileStream = new FileStrem(filePath, FileMode.Open))
            {
                await fileStream.CopyToAsync(memory;)
            }

            memory.Position = 0;
        }

        return memory;
    }
}