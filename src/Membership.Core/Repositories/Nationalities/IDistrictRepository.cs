using Membership.Core.Entities.Nationalities;

namespace Membership.Core.Repositories.Nationalities;

public interface IDistrictRepository
{
    Task<District> GetByIdAsync(Guid id);
    Task<IEnumerable<District>> GetAsync();
    Task AddAsync(District district);
    Task UpdateAsync(District district);
}