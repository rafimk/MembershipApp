namespace Membership.Infrastructure;

public sealed class FileUploadOptions
{
    public string FilePath { get; set; }
    public string PermittedExtension { get; set; }
    public string MaxSize { get; set; }
}