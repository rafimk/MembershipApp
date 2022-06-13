using Membership.Core.Entities.Nationalities;
using Membership.Core.ValueObjects;

namespace Membership.Core.Repositories.Nationalities;

public interface IAreaRepository
{
    Task<Area> GetByIdAsync(GenericId id);
    Task<IEnumerable<Area>> GetByStateIdAsync(GenericId stateId);
    Task<IEnumerable<Area>> GetAsync();
    Task AddAsync(Area area);
    Task UpdateAsync(Area area);
}