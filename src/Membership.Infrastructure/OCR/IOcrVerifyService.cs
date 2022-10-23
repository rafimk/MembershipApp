namespace Membership.Infrastructure.OCR;

public interface IOcrVerifyService
{
    Task<string> PassportService(string filePath, string fileInfo);
}