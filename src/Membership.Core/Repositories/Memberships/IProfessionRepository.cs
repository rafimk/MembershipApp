using Membership.Core.Entities.Memberships.Professions;

namespace Membership.Core.Repositories.Memberships;

public interface IProfessionRepository
{
    Task<Profession> GetByIdAsync(Guid id);
    Task<IEnumerable<Profession>> GetAsync();
    Task AddAsync(Profession profession);
    Task UpdateAsync(Profession profession);
    Task DeleteAsync(Guid id);
}