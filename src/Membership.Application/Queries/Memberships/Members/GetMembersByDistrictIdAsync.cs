using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;

namespace Membership.Application.Queries.Memberships.Members;

public class GetMembersByDistrictIdAsync : IQuery<IEnumerable<MemberDto>>
{
    public Guid DistrictId { get; set; }
}