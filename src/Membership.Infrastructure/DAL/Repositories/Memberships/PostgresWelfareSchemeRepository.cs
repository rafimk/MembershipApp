using Membership.Core.Entities.Memberships.Professions;
using Membership.Core.Repositories.Memberships;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Repositories.Memberships;

internal sealed class PostgresWelfareSchemeRepository : IWelfareSchemeRepository
{
    private readonly MembershipDbContext _dbContext;

    public PostgresWelfareSchemeRepository(MembershipDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public  Task<WelfareScheme> GetByIdAsync(GenericId id)
        => _dbContext.WelfareSchemes.SingleOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<WelfareScheme>> GetAsync()
        => await _dbContext.WelfareSchemes.Where(x => !x.IsDeleted).ToListAsync();

    public async Task AddAsync(WelfareScheme welfareScheme)
    {
        await _dbContext.WelfareSchemes.AddAsync(welfareScheme);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(WelfareScheme welfareScheme)
    {
        _dbContext.WelfareSchemes.Update(welfareScheme);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(GenericId id)
    {
        var welfareScheme = await GetByIdAsync(id);
        welfareScheme.Delete();
        _dbContext.WelfareScheme.Update(welfareScheme);
        await _dbContext.SaveChangesAsync();
    }
}