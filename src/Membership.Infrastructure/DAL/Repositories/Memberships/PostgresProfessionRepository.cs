using Membership.Core.Entities.Nationalities;
using Membership.Core.Repositories.Nationalities;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Repositories.Memberships;

internal sealed class PostgresProfessionRepository : IProfessionRepository
{
    private readonly MMSDbContext _dbContext;

    public PostgresProfessionRepository(MMSDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public  Task<Profession> GetByIdAsync(GenericId id)
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

    public async Task DeleteAsync(GenericId id)
    {
        var profession = await GetByIdAsync(id);
        profession.Delete();
        _dbContext.Professions.Update(profession);
        await _dbContext.SaveChangesAsync();
    }
}