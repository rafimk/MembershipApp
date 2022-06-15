using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;

namespace Membership.Application.Queries.Memberships.Members;

public class GetMembersByAreaIdAsync : IQuery<IEnumerable<MemberDto>>
{
    public Guid AreaId { get; set; }
}