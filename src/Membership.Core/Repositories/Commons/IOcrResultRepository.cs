using Membership.Core.Entities.Commons;

namespace Membership.Core.Repositories.Commons;

public interface IOcrResultRepository
{
    Task<OcrResult> GetByIdAsync(Guid id);
    Task<OcrResult> GetByFrontPageIdAsync(Guid? frontPageId);
    Task<OcrResult> GetByLastPageIdAsync(Guid? lastPageId);
    Task AddAsync(OcrResult ocrResult);
    Task UpdateAsync(OcrResult ocrResult);
}