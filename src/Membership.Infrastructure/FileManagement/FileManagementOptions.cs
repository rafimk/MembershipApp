namespace Membership.Infrastructure.FileManagement;

public sealed class FileManagementOptions
{
    public string SharedRootPath { get; set; }
    public long MaxFileSize { get; set; }
    public string[] Extensions { get; set; }
}