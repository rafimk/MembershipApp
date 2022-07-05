using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;

namespace Membership.Application.Queries.Memberships.Disputes;

public class GetDisputeRequestByMandalamId : IQuery<DisputeRequestDto>
{
    public Guid MandalamId { get; set; }
}