using Membership.Core.Entities.Commons;

namespace Membership.Core.Repositories.Commons;

public interface IOCRResultRepository
{
    Task<OCRResult> GetByIdAsync(Guid id);
    Task AddAsync(OCRResult ocrResult);
    Task UpdateAsync(OCRResult ocrResult);
}