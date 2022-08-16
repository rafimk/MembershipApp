namespace Membership.Core.Contracts.Memberships;

public record DisputeApprovalContract()
{
    public Guid StateId { get; set; }
    public Guid ProposedAreaId { get; set; }
    public Guid DistrictId { get; set; }
    public Guid ProposedMandalamId { get; set; }
    public Guid ProposedPanchayatId { get; set; }
}