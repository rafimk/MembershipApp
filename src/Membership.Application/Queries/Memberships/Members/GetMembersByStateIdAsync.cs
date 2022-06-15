using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;

namespace Membership.Application.Queries.Memberships.Members;

public class GetMembersByStateIdAsync : IQuery<IEnumerable<MemberDto>>
{
    public Guid StateId { get; set; }
}