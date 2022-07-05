using Membership.Core.Entities.Nationalities;
using Membership.Core.Repositories.Nationalities;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Repositories.Nationalities;

internal sealed class PostgresMandalamRepository : IMandalamRepository
{
    private readonly MembershipDbContext _dbContext;

    public PostgresMandalamRepository(MembershipDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public  Task<Mandalam> GetByIdAsync(GenericId id)
        => _dbContext.Mandalams.Include(x => x.District).SingleOrDefaultAsync(x => x.Id == id);
    
    public async Task<IEnumerable<Mandalam>> GetByDistrictIdAsync(GenericId districtId)
        => await _dbContext.Mandalams.Where(x => x.DistrictId == districtId && !x.IsDeleted).ToListAsync();
    
    public async Task<IEnumerable<Mandalam>> GetAsync()
        => await _dbContext.Mandalams.Where(x => !x.IsDeleted).ToListAsync();

    public async Task AddAsync(Mandalam mandalam)
    {
        await _dbContext.Mandalams.AddAsync(mandalam);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Mandalam mandalam)
    {
        _dbContext.Mandalams.Update(mandalam);
        await _dbContext.SaveChangesAsync();
    }
}