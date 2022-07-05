using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;

namespace Membership.Application.Queries.Memberships.Disputes;

public class GetDisputeRequestsByAreaId : IQuery<IEnumerable<DisputeRequestDto>>
{
    public Guid AreaId { get; set; }
}