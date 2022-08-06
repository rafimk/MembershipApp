using Membership.Core.Entities.Memberships.RegisteredOrganizations;
using Membership.Core.Repositories.Memberships;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Repositories.Memberships;

internal sealed class PostgresRegisteredOrganizationRepository : IRegisteredOrganizationRepository
{
    private readonly MembershipDbContext _dbContext;

    public PostgresRegisteredOrganizationRepository(MembershipDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public  Task<RegisteredOrganization> GetByIdAsync(Guid id)
        => _dbContext.RegisteredOrganizations.SingleOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<RegisteredOrganization>> GetAsync()
        => await _dbContext.RegisteredOrganizations.Where(x => !x.IsDeleted).ToListAsync();

    public async Task AddAsync(RegisteredOrganization registeredOrganization)
    {
        await _dbContext.RegisteredOrganizations.AddAsync(registeredOrganization);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(RegisteredOrganization registeredOrganization)
    {
        _dbContext.RegisteredOrganizations.Update(registeredOrganization);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var registeredOrganization = await GetByIdAsync(id);
        registeredOrganization.Delete();
        _dbContext.RegisteredOrganizations.Update(registeredOrganization);
        await _dbContext.SaveChangesAsync();
    }
}