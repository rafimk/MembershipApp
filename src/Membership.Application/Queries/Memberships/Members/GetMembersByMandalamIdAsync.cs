using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;

namespace Membership.Application.Queries.Memberships.Members;

public class GetMembersByMandalamIdAsync : IQuery<IEnumerable<MemberDto>>
{
    public Guid MandalamId { get; set; }
}