namespace Membership.Core.Contracts.Memberships;

public record DisputeApprovalContract()
{
    public Guid ToStateId { get; set; }
    public Guid ToAreaId { get; set; }
    public Guid ToDistrictId { get; set; }
    public Guid ToMandalamId { get; set; }
    public Guid ToPanchayatId { get; set; }
    public Guid? AgentId { get; set; }
}