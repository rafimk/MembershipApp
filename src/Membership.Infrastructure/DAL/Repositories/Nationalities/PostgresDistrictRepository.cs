using Membership.Core.Entities.Nationalities;
using Membership.Core.Repositories.Nationalities;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Repositories.Nationalities;

internal sealed class PostgresDistrictRepository : IDistrictRepository
{
    private readonly MMSDbContext _dbContext;

    public PostgresDistrictRepository(MMSDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public  Task<District> GetByIdAsync(GenericId id)
        => _dbContext.Districts.SingleOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<District>> GetAsync()
        => await _dbContext.Districts.Where(x => !x.IsDeleted).ToListAsync();

    public async Task AddAsync(District district)
    {
        await _dbContext.Districts.AddAsync(district);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(District district)
    {
        _dbContext.Districts.Update(district);
        await _dbContext.SaveChangesAsync();
    }
}