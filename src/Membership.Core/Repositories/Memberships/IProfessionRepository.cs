using Membership.Core.Entities.Memberships.Professions;
using Membership.Core.ValueObjects;

namespace Membership.Core.Repositories.Memberships;

public interface IProfessionRepository
{
    Task<Profession> GetByIdAsync(GenericId id);
    Task<IEnumerable<Profession>> GetAsync();
    Task AddAsync(Profession profession);
    Task UpdateAsync(Profession profession);
    Task DeleteAsync(GenericId id);
}