using Membership.Core.Entities.Memberships.Members;
using Membership.Core.ValueObjects;

namespace Membership.Core.Repositories.Memberships;

public interface IMemberRepository
{
    Task<Member> GetByIdAsync(Guid id);
    Task<Member> GetByMembershipIdAsync(string membershipId);
    Task<Member> GetByEmiratesIdAsync(EmiratesIdNumber emiratesId);
    Task<Member> GetByEmailAsync(Email email);
    Task<Member> GetNextMemberForVerification();
    Task<IEnumerable<Member>> GetAsync();
    string GenerateMembershipId(string prefix);
    Task AddAsync(Member member);
    Task UpdateAsync(Member member);
}