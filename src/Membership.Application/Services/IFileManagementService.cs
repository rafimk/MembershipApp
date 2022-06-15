namespace Membership.Application.Services;

public interface IFileManagementService
{
    Task<Guid> Upload(List<IFromFile> files, string fileUploadPath, FileType Type, CancellationToken CancellationToken = default);
    Task<MemoryStream> Download(Guid fileId, string fileUploadPath, FileType Type, CancellationToken CancellationToken = default);
}