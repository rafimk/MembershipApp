using Membership.Core.Entities.Commons;

namespace Membership.Infrastructure.OCR;

public interface IOcrService
{
    Task<OcrResult> ReadData(string fileInfo);
}