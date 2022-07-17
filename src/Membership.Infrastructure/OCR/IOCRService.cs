using Membership.Core.Entities.Commons;

namespace Membership.Infrastructure.OCR;

public interface IOcrService
{
    Task<OcrData> ReadData(string fileInfo, Guid userId);
}