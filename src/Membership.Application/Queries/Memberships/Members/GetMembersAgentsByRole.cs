using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;

namespace Membership.Application.Queries.Memberships.Members;

public class GetMembersAgentsByRole : IQuery<IEnumerable<AgentListDto>>
{
    public Guid? UserId { get; set; }
}