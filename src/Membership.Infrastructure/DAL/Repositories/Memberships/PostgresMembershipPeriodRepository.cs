using Membership.Core.Entities.Nationalities;
using Membership.Core.Repositories.Nationalities;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Repositories.Memberships;

internal sealed class PostgresMembershipPeriodRepository : IMembershipPeriodRepository
{
    private readonly MMSDbContext _dbContext;

    public PostgresMembershipPeriodRepository(MMSDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public  Task<MembershipPeriod> GetByIdAsync(GenericId id)
        => _dbContext.MembershipPeriods.SingleOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<MembershipPeriod>> GetAsync()
        => await _dbContext.MembershipPeriods.Where(x => x.IsActive).ToListAsync();
    
    public  Task<MembershipPeriod> GetActivePeriodAsync()
        => _dbContext.MembershipPeriods.SingleOrDefaultAsync(x => x.IsActive);

    public async Task AddAsync(MembershipPeriod membershipPeriod)
    {
        await _dbContext.MembershipPeriods.AddAsync(membershipPeriod);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(MembershipPeriod membershipPeriod)
    {
        _dbContext.MembershipPeriods.Update(membershipPeriod);
        await _dbContext.SaveChangesAsync();
    }

    public async Task ActiveAsync(GenericId id)
    {
        var membershipPeriod = await GetByIdAsync(id);
        membershipPeriod.Activate();
        _dbContext.MembershipPeriods.Update(membershipPeriod);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task InActive(GenericId id)
    {
        var membershipPeriod = await GetByIdAsync(id);
        membershipPeriod.Deactivate();
        _dbContext.MembershipPeriods.Update(membershipPeriod);
        await _dbContext.SaveChangesAsync();
    }
}