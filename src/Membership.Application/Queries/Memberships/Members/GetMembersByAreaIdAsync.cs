using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;

namespace Membership.Application.Queries.Memberships.Members;

public class GetMembersByAreaIdAsync : IQuery<IEnumerable<MemberDto>>
{
    public Guid AreaId { get; set; }
}