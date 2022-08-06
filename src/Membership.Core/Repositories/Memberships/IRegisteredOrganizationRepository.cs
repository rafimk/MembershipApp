using Membership.Core.Entities.Memberships.RegisteredOrganizations;

namespace Membership.Core.Repositories.Memberships;

public interface IRegisteredOrganizationRepository
{
    Task<RegisteredOrganization> GetByIdAsync(Guid id);
    Task<IEnumerable<RegisteredOrganization>> GetAsync();
    Task AddAsync(RegisteredOrganization registeredOrganization);
    Task UpdateAsync(RegisteredOrganization registeredOrganization);
    Task DeleteAsync(Guid id);
}