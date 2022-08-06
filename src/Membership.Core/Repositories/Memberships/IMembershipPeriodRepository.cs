using Membership.Core.Entities.Memberships.MembershipPeriods;

namespace Membership.Core.Repositories.Memberships;

public interface IMembershipPeriodRepository
{
    Task<MembershipPeriod> GetByIdAsync(Guid id);
    Task<IEnumerable<MembershipPeriod>> GetAsync();
    Task<MembershipPeriod> GetActivePeriodAsync();
    Task AddAsync(MembershipPeriod membershipPeriod);
    Task UpdateAsync(MembershipPeriod membershipPeriod);
}