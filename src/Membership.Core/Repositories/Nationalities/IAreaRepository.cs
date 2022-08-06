using Membership.Core.Entities.Nationalities;

namespace Membership.Core.Repositories.Nationalities;

public interface IAreaRepository
{
    Task<Area> GetByIdAsync(Guid id);
    Task<IEnumerable<Area>> GetByStateIdAsync(Guid stateId);
    Task<IEnumerable<Area>> GetAsync();
    Task AddAsync(Area area);
    Task UpdateAsync(Area area);
}