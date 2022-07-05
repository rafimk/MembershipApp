using Membership.Application.DTO.Nationalities;

namespace Membership.Application.DTO.Memberships;

public class DisputeRequestDto
{
    public Guid Id { get; set; }
    public Guid MemberId { get; set; }
    public MemberDto Member { get; set; }
    public Guid ProposedAreaId { get; set; }
    public AreaDto ProposedArea { get; set; }
    public Guid ProposedMandalamId { get; set; }
    public MandalamDto ProposedMandalam { get; set; }
    public Guid ProposedPanchayatId { get; set; }
    public PanchayatDto ProposedPanchayat { get; set; }
    public string Reason { get; set; }
    public string JustificationComment { get; set; }
    public DateTimeOffset SubmittedDate  { get; set; }
    public Guid SubmittedBy { get; set; }
    public DateTimeOffset? ActionDate  { get; set; }
    public Guid? ActionBy { get; set; }
    public int Status { get; set; }
}
