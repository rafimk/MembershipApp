using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;

namespace Membership.Application.Queries.Memberships.Disputes;

public class GetDisputeRequestById : IQuery<DisputeRequestDto>
{
    public Guid RequestId { get; set; }
}