using Membership.Core.Consts;

namespace Membership.Core.Entities.Commons;

public class FileAttachment
{
    public Guid Id { get; set; }
    public Guid RelatedId { get; set; }
    public FileAttachmentType Type { get; set; }
    public string ActualFileName { get; set; }
    public string SavedFileName { get; set; }
    public string FileExtension { get; set; }
    public string FileType { get; set; }
    public string FilePath { get; set; }
    public long FileSize { get; set; }
    public bool IsDeleted { get; set; }
}