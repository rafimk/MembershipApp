using Membership.Core.Entities.Commons;
using Membership.Core.Repositories.Commons;
using Membership.Infrastructure.DAL.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Repositories.Commons;

internal sealed class PostgresOcrResultRepository : IOcrResultRepository
{
    private readonly MembershipDbContext _dbContext;

    public PostgresOcrResultRepository(MembershipDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public  Task<OcrResult> GetByIdAsync(Guid id)
        => _dbContext.OcrResults.SingleOrDefaultAsync(x => x.Id == id);

   public async Task AddAsync(OcrResult ocrResult)
    {
        await _dbContext.OcrResults.AddAsync(ocrResult);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(OcrResult ocrResult)
    {
        var entity = await _dbContext.OcrResults.SingleOrDefaultAsync(x => x.Id == ocrResult.Id);

        if (entity is null)
        {
            throw new OcrResultNotFoundException(ocrResult.Id);
        }

        // Check If not null then update each property.
        
        _dbContext.Update(ocrResult);
        await _dbContext.SaveChangesAsync();
    }
}