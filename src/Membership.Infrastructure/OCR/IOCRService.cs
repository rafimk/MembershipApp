using Membership.Core.Entities.Commons;

namespace Membership.Core.Repositories.Commons;

public interface IOCRService
{
    Task<OCRResult> ReadData(string fileInfo);
}