using Membership.Core.Entities.Memberships.RegisteredOrganizations;
using Membership.Core.Entities.Memberships.WelfareSchemes;
using Membership.Core.ValueObjects;

namespace Membership.Core.Repositories.Memberships;

public interface IWelfareSchemeRepository
{
    Task<WelfareScheme> GetByIdAsync(GenericId id);
    Task<IEnumerable<WelfareScheme>> GetAsync();
    Task AddAsync(WelfareScheme welfareScheme);
    Task UpdateAsync(WelfareScheme welfareScheme);
    Task DeleteAsync(GenericId id);
}