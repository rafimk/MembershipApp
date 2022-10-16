using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;

namespace Membership.Application.Queries.Memberships.Members;

public record GetMembersByRoleAsExcel : IQuery<IEnumerable<MemberListDto>>
{
    public Guid? UserId { get; set; }
    public int? SearchType { get; set; }
    public string SearchString { get; set; }
}