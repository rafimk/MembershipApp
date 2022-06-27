using Membership.Core.Consts;
using Microsoft.AspNetCore.Http;

namespace Membership.Infrastructure.FileManagement;

public interface IBufferedFileUploadService
{
    Task<Guid?> UploadFile(IFormFile file, string filePath, FileType type);

    Task<MemoryStream> Download(Guid fileGuid, string fileName, string filePath);
}