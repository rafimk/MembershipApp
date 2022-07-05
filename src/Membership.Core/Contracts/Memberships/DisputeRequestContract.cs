using Membership.Core.Consts;
using Membership.Core.ValueObjects;

namespace Membership.Core.Contracts.Memberships;

public record DisputeRequestContract()
{
    public GenericId Id { get; set; }
    public GenericId MemberId { get; set; }
    public GenericId ProposedAreaId { get; set; }
    public GenericId ProposedMandalamId { get; set; }
    public GenericId ProposedPanchayatId { get; set; }
    public string Reason { get; set; }
    public string JustificationComment { get; set; }
    public DateTime SubmittedDate  { get; set; }
    public GenericId SubmittedBy { get; set; }
    public DateTime? ActionDate  { get; set; }
    public Guid? ActionBy { get; set; }
    public DisputeStatus Status { get; set; }
}