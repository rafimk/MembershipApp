using Membership.Core.Consts;

namespace Membership.Infrastructure.FileManagement;

public record FileInfoDto
{
    public Guid Id { get; set; }
    public FileType Type { get; set; }
    public string ActualFileName { get; set; }
    public string SavedFileName { get; set; }
    public string FileExtension { get; set;}
    public string FileType { get; set; }
    public string FilePath { get; set; }
    public long FileSize { get; set; }
}