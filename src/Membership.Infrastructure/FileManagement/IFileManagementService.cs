using Membership.Core.Consts;

namespace Membership.Infrastructure.FileManagement;

public class IFileManagementService
{
    Task<Guid> Upload(List<IFromFile> files, string fileUploadPath, FileType Type, CancellationToken CancellationToken = default);
    Task<MemoryStream> Download(Guid fileId, string fileUploadPath, FileType Type, CancellationToken CancellationToken = default);
}
