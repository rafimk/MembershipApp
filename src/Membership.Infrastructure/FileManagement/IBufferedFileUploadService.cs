using Membership.Application.DTO.Commons;
using Membership.Core.Consts;
using Microsoft.AspNetCore.Http;

namespace Membership.Infrastructure.FileManagement;

public interface IBufferedFileUploadService
{
    Task<Guid?> UploadFile(IFormFile file, string filePath, FileType type);

    Task<BufferedFileUploadDto> Download(Guid fileGuid, string filePath);
}