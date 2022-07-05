using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;

namespace Membership.Application.Queries.Memberships.Disputes;

public class GetDisputeRequestsByStateId : IQuery<DisputeRequestDto>
{
    public Guid StateId { get; set; }
}