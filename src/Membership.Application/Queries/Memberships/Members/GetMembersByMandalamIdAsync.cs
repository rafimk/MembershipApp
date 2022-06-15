using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;

namespace Membership.Application.Queries.Memberships.Members;

public class GetMembersByMandalamIdAsync : IQuery<IEnumerable<MemberDto>>
{
    public Guid MandalamId { get; set; }
}