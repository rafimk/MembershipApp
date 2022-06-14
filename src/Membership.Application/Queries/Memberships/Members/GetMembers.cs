using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;

namespace Membership.Application.Queries.Memberships.Members;

public class GetMembers : IQuery<IEnumerable<MemberDto>>
{
    public Guid MemberId { get; set; }
}