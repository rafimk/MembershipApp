using Membership.Core.Entities.Memberships.Members;

namespace Membership.Core.Repositories.Memberships;

public interface IMembershipVerificationRepository
{
    Task<MembershipVerification> GetByIdAsync(Guid id);
    Task<MembershipVerification> GetByMemberIdAsync(Guid memberId);
    Task<IEnumerable<MembershipVerification>> GetAsync();
    Task AddAsync(MembershipVerification membershipVerification);
    Task UpdateAsync(MembershipVerification membershipVerification);
}