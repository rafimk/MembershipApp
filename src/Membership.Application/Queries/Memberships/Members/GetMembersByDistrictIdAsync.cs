using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;

namespace Membership.Application.Queries.Memberships.Members;

public class GetMembersByDistrictIdAsync : IQuery<IEnumerable<MemberDto>>
{
    public Guid DistrictId { get; set; }
}