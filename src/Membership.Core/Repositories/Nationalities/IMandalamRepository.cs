using Membership.Core.Entities.Nationalities;

namespace Membership.Core.Repositories.Nationalities;

public interface IMandalamRepository
{
    Task<Mandalam> GetByIdAsync(Guid id);
    Task<IEnumerable<Mandalam>> GetByDistrictIdAsync(Guid districtId);
    Task<IEnumerable<Mandalam>> GetAsync();
    Task AddAsync(Mandalam mandalam);
    Task UpdateAsync(Mandalam mandalam);
}