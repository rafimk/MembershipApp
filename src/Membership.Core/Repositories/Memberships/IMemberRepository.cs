using Membership.Core.Entities.Memberships.Members;
using Membership.Core.ValueObjects;

namespace Membership.Core.Repositories.Memberships;

public interface IMemberRepository
{
    Task<Member> GetByIdAsync(GenericId id);
    Task<Member> GetByMemberIdAsync(MembershipId membershipId);
    Task<Member> GetByEmiratesIdAsync(EmiratesId emiratesId);
    Task<IEnumerable<Member>> GetAsync();
    Task AddAsync(Member member);
    Task UpdateAsync(Member member);
}