using Membership.Core.Entities.Nationalities;
using Membership.Core.ValueObjects;

namespace Membership.Core.Repositories.Nationalities;

public interface IDistrictRepository
{
    Task<District> GetByIdAsync(GenericId id);
    Task<IEnumerable<District>> GetAsync();
    Task AddAsync(District district);
    Task UpdateAsync(District district);
}