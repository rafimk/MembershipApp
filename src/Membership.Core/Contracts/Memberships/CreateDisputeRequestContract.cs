namespace Membership.Core.Contracts.Memberships;

public record CreateDisputeRequestContract()
{
    public Guid Id { get; set; }
    public Guid MemberId { get; set; }
    public Guid ToStateId { get; set; }
    public Guid ToAreaId { get; set; }
    public Guid ToDistrictId { get; set; }
    public Guid ToMandalamId { get; set; }
    public Guid ToPanchayatId { get; set; }
    public Guid FromStateId { get; set; }
    public Guid FromAreaId { get; set; }
    public Guid FromDistrictId { get; set; }
    public Guid FromMandalamId { get; set; }
    public Guid FromPanchayatId { get; set; }
    public string Reason { get; set; }
    public DateTime SubmittedDate  { get; set; }
    public Guid SubmittedBy { get; set; }
}