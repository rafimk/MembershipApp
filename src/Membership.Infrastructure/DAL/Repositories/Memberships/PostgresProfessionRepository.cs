using Membership.Core.Entities.Memberships.Professions;
using Membership.Core.Repositories.Memberships;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Repositories.Memberships;

internal sealed class PostgresProfessionRepository : IProfessionRepository
{
    private readonly MembershipDbContext _dbContext;

    public PostgresProfessionRepository(MembershipDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public  Task<Profession> GetByIdAsync(Guid id)
        => _dbContext.Professions.SingleOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<Profession>> GetAsync()
        => await _dbContext.Professions.Where(x => !x.IsDeleted).ToListAsync();

    public async Task AddAsync(Profession profession)
    {
        await _dbContext.Professions.AddAsync(profession);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Profession profession)
    {
        _dbContext.Professions.Update(profession);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var profession = await GetByIdAsync(id);
        profession.Delete();
        _dbContext.Professions.Update(profession);
        await _dbContext.SaveChangesAsync();
    }
}