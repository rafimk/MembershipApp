using Membership.Core.Entities.Nationalities;
using Membership.Core.Repositories.Nationalities;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Repositories.Nationalities;

internal sealed class PostgresAreaRepository : IAreaRepository
{
    private readonly MembershipDbContext _dbContext;

    public PostgresAreaRepository(MembershipDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public  Task<Area> GetByIdAsync(Guid id)
        => _dbContext.Areas.Include(x => x.State).SingleOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<Area>> GetByStateIdAsync(Guid stateId)
        => await _dbContext.Areas.Where(x => x.StateId == stateId && !x.IsDeleted).ToListAsync();
    
    public async Task<IEnumerable<Area>> GetAsync()
        => await _dbContext.Areas.Where(x => !x.IsDeleted).ToListAsync();

    public async Task AddAsync(Area area)
    {
        await _dbContext.Areas.AddAsync(area);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Area area)
    {
        _dbContext.Areas.Update(area);
        await _dbContext.SaveChangesAsync();
    }
}