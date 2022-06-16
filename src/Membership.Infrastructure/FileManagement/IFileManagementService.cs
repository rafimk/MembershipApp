using Microsoft.AspNetCore.Http;
using Membership.Core.Consts;

namespace Membership.Infrastructure.FileManagement;

public class IFileManagementService
{
    Task<bool> UploadFile(IFormFile file, string fileUploadPath, FileType Type, CancellationToken CancellationToken = default);
    Task<Guid> UploadFiles(List<IFromFile> files, string fileUploadPath, FileType Type, CancellationToken CancellationToken = default);
    Task<MemoryStream> Download(Guid fileId, string fileUploadPath, FileType Type, CancellationToken CancellationToken = default);
}
