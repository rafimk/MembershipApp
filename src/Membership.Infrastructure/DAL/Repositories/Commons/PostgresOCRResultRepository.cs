using Membership.Core.Entities.Commons;
using Membership.Core.Repositories.Commons;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Repositories.Commons;

internal sealed class PostgresOCRResultRepository : IOCRResultRepository
{
    private readonly MembershipDbContext _dbContext;

    public PostgresOCRResultRepository(MembershipDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public  Task<OCRResult> GetByIdAsync(Guid id)
        => _dbContext.OCRResults.SingleOrDefaultAsync(x => x.Id == id);

   public async Task AddAsync(OCRResult ocrResult)
    {
        await _dbContext.OCRResults.AddAsync(ocrResult);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(OCRResult ocrResult)
    {
        var ocrResult = await _dbContext.OCRResults.SingleOrDefaultAsync(x => x.Id == ocrResult.id);

        if (ocrResult is null)
        {
            throw new OCRResultNotFoundException();
        }

        // Check If not null then update each property.
        
        _dbContext.Update(ocrResult);
        await _dbContext.SaveChangesAsync();
    }
}