namespace Membership.Infrastructure.OCR;

public interface IOcrVerifyService
{
    Task PassportService(string filePath, string fileInfo);
}