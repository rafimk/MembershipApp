using Membership.Core.ValueObjects;

namespace Membership.Core.Contracts.Memberships;

public record UpdateDisputeRequestContract()
{
    public GenericId ProposedAreaId { get; set; }
    public string Reason { get; set; }
}