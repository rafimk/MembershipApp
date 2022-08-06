namespace Membership.Core.Contracts.Memberships;

public record CreateDisputeRequestContract()
{
    public Guid Id { get; set; }
    public Guid MemberId { get; set; }
    public Guid ProposedAreaId { get; set; }
    public Guid ProposedMandalamId { get; set; }
    public Guid ProposedPanchayatId { get; set; }
    public string Reason { get; set; }
    public DateTime SubmittedDate  { get; set; }
    public Guid SubmittedBy { get; set; }
}