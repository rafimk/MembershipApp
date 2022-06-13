using Membership.Core.Entities.Memberships.MembershipPeriods;
using Membership.Core.ValueObjects;

namespace Membership.Core.Repositories.Memberships;

public interface IMembershipPeriodRepository
{
    Task<MembershipPeriod> GetByIdAsync(GenericId id);
    Task<IEnumerable<MembershipPeriod>> GetAsync();
    Task<MembershipPeriod> GetActivePeriodAsync();
    Task AddAsync(MembershipPeriod membershipPeriod);
    Task UpdateAsync(MembershipPeriod membershipPeriod);
}