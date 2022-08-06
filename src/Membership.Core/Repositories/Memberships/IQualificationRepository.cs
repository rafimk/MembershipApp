using Membership.Core.Entities.Memberships.Qualifications;

namespace Membership.Core.Repositories.Memberships;

public interface IQualificationRepository
{
    Task<Qualification> GetByIdAsync(Guid id);
    Task<IEnumerable<Qualification>> GetAsync();
    Task AddAsync(Qualification qualification);
    Task UpdateAsync(Qualification qualification);
    Task DeleteAsync(Guid id);
}