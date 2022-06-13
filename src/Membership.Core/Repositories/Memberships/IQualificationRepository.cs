using Membership.Core.Entities.Memberships.Qualifications;
using Membership.Core.ValueObjects;

namespace Membership.Core.Repositories.Memberships;

public interface IQualificationRepository
{
    Task<Qualification> GetByIdAsync(GenericId id);
    Task<IEnumerable<Qualification>> GetAsync();
    Task AddAsync(Qualification qualification);
    Task UpdateAsync(Qualification qualification);
    Task DeleteAsync(GenericId id);
}