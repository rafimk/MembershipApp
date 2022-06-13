using Membership.Core.Entities.Memberships.Members;
using Membership.Core.ValueObjects;

namespace Membership.Core.Repositories.Memberships;

public interface IMemberRepository
{
    Task<Member> GetByIdAsync(GenericId id);
    Task<Member> GetByMembershipIdAsync(MembershipId membershipId);
    Task<IEnumerable<Member>> GetAsync();
    Task<IEnumerable<Member>> GetActiveAsync();
    Task<IEnumerable<Member>> GetByAreaIdAsync(GenericId areaId);
    Task<IEnumerable<Member>> GetByMandalamIdAsync(GenericId mandalamId);
    Task AddAsync(Member member);
    Task UpdateAsync(Member member);
    Task ActivateAsync(GenericId id);
    Task DeactivateAsync(GenericId id);
}