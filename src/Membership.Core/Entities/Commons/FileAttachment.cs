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
    public Guid? MemberId { get; private set; }
    public Guid? UserId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string OcrData { get; private set; }

    private FileAttachment(Guid id, FileType type, string actualFileName, string savedFileName, string fileExtension,
        string fileType,  string filePath, long fileSize, bool isDeleted, DateTime createdAt, Guid? userId)
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
        UserId = userId;
        OcrData = null;
    }

    private FileAttachment()
    {
    }
    
    public static FileAttachment Create(Guid id, FileType type, string actualFileName, string savedFileName ,string fileExtension,
        string fileType,  string filePath, long fileSize, DateTime createdAt, Guid? userId)
        => new(id, type, actualFileName, savedFileName, fileExtension, fileType, filePath, fileSize, false, createdAt, userId);
    
    public void Delete()
    {
        IsDeleted = true;
    }
    
    public void UpdateOcrData(string ccrData)
    {
        OcrData = ccrData;
    }
    
    public void UpdateMemberId(Guid memberId)
    {
        MemberId = memberId;
    }
    
    public void AssignToMember(Guid memberId)
    {
        MemberId = memberId;
    }
}