using Membership.Core.Entities.Nationalities;
using Membership.Core.ValueObjects;

namespace Membership.Core.Repositories.Nationalities;

public interface IPanchayatRepository
{
    Task<Panchayat> GetByIdAsync(GenericId id);
    Task<IEnumerable<Panchayat>> GetByMandalamIdAsync(GenericId mandalamId);
    Task<IEnumerable<Panchayat>> GetAsync();
    Task AddAsync(Panchayat panchayat);
    Task UpdateAsync(Panchayat panchayat);
}