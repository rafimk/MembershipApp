using Membership.Core.Consts;

namespace Membership.Core.Entities.Commons;

public class FileAttachment
{
    public Guid Id { get; private set; }
    public FileType Type { get; private set; }
    public string ActualFileName { get; private set; }
    public string SavedFileName { get; private set; }
    public string FileExtension { get; private set; }
    public string FileType { get; private set; }
    public string FilePath { get; private set; }
    public long FileSize { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private FileAttachment(Guid id, FileType type, string actualFileName, string fileExtension,
        string fileType,  string filePath, long fileSize, bool isDeleted, DateTime createdAt)
    {
        Id = id;
        Type = type;
        ActualFileName = actualFileName;
        SavedFileName = savedFileName;
        FileExtension = fileExtension;
        FileType = fileType;
        FilePath = filePath;
        FileSize = fileSize;
        IsDeleted = isDeleted;
        CreatedAt = createdAt;
    }

    private FileAttachment()
    {
    }
    
    public static Profession Create(Guid id, FileType type, string actualFileName, string fileExtension,
        string fileType,  string filePath, long fileSize, DateTime createdAt)
        => new(id, type, actualFileName, fileExtension, fileType, filePath, false, createdAt);
    
    public void Delete()
    {
        IsDeleted = true;
    }
}