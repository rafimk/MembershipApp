using Membership.Core.Entities.Nationalities;

namespace Membership.Core.Repositories.Nationalities;

public interface IPanchayatRepository
{
    Task<Panchayat> GetByIdAsync(Guid id);
    Task<IEnumerable<Panchayat>> GetByMandalamIdAsync(Guid mandalamId);
    Task<IEnumerable<Panchayat>> GetAsync();
    Task AddAsync(Panchayat panchayat);
    Task UpdateAsync(Panchayat panchayat);
}