using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;

namespace Membership.Application.Queries.Memberships.Disputes;

public class GetDisputeRequestByRole : IQuery<IEnumerable<DisputeRequestDto>>
{
    public Guid UserId { get; set; }
}