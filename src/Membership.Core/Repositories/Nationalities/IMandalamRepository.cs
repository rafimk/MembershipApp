using Membership.Core.Entities.Nationalities;
using Membership.Core.ValueObjects;

namespace Membership.Core.Repositories.Nationalities;

public interface IMandalamRepository
{
    Task<Mandalam> GetByIdAsync(GenericId id);
    Task<IEnumerable<Mandalam>> GetByDistrictIdAsync(GenericId districtId);
    Task<IEnumerable<Mandalam>> GetAsync();
    Task AddAsync(Mandalam mandalam);
    Task UpdateAsync(Mandalam mandalam);
}