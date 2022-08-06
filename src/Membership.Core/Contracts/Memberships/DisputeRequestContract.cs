using Membership.Core.Consts;

namespace Membership.Core.Contracts.Memberships;

public record DisputeRequestContract()
{
    public Guid Id { get; set; }
    public Guid MemberId { get; set; }
    public Guid ProposedAreaId { get; set; }
    public Guid ProposedMandalamId { get; set; }
    public Guid ProposedPanchayatId { get; set; }
    public string Reason { get; set; }
    public string JustificationComment { get; set; }
    public DateTime SubmittedDate  { get; set; }
    public Guid SubmittedBy { get; set; }
    public DateTime? ActionDate  { get; set; }
    public Guid? ActionBy { get; set; }
    public DisputeStatus Status { get; set; }
}