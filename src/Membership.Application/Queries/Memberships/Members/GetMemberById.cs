using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;

namespace Membership.Application.Queries.Memberships.Members;

public class GetMemberById : IQuery<MemberDto>
{
    public Guid MemberId { get; set; }
}