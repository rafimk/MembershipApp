using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;

namespace Membership.Application.Queries.Memberships.Disputes;

public class GetDisputeRequestByMandalamId : IQuery<IEnumerable<DisputeRequestDto>>
{
    public Guid MandalamId { get; set; }
}