using Membership.Core.Entities.Nationalities;
using Membership.Core.Repositories.Nationalities;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Repositories.Nationalities;

internal sealed class PostgresPanchayatRepository : IPanchayatRepository
{
    private readonly MembershipDbContext _dbContext;

    public PostgresPanchayatRepository(MembershipDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public  Task<Panchayat> GetByIdAsync(GenericId id)
        => _dbContext.Panchayats.Include(x => x.Mandalam).SingleOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<Panchayat>> GetByMandalamIdAsync(GenericId mandalamId)
        => await _dbContext.Panchayats.Where(x => x.MandalamId == mandalamId && !x.IsDeleted).ToListAsync();
    
    public async Task<IEnumerable<Panchayat>> GetAsync()
        => await _dbContext.Panchayats.Where(x => !x.IsDeleted).ToListAsync();

    public async Task AddAsync(Panchayat Panchayat)
    {
        await _dbContext.Panchayats.AddAsync(Panchayat);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Panchayat Panchayat)
    {
        _dbContext.Panchayats.Update(Panchayat);
        await _dbContext.SaveChangesAsync();
    }
}