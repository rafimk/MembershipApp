using Membership.Core.Entities.Memberships.Professions;
using Membership.Core.ValueObjects;

namespace Membership.Core.Repositories.Memberships;

public interface IRegisteredOrganizationRepository
{
    Task<RegisteredOrganization> GetByIdAsync(GenericId id);
    Task<IEnumerable<RegisteredOrganization>> GetAsync();
    Task AddAsync(RegisteredOrganization registeredOrganization);
    Task UpdateAsync(RegisteredOrganization registeredOrganization);
    Task DeleteAsync(GenericId id);
}