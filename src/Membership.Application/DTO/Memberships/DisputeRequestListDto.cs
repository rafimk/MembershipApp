using Membership.Application.DTO.Nationalities;

namespace Membership.Application.DTO.Memberships;

public class DisputeRequestListDto
{
    public Guid Id { get; set; }
    public Guid MemberId { get; set; }
    public string MembershipId { get; set; }
    public string FullName { get; set; }
    public string EmiratesIdNumber { get; set; }
    public string MobileNumber { get; set; }
    public StateDto ToState { get; set; }
    public AreaDto ToArea { get; set; }
    public DistrictDto ToDistrict { get; set; }
    public MandalamDto ToMandalam { get; set; }
    public PanchayatDto ToPanchayat { get; set; }
    public StateDto FromState { get; set; }
    public AreaDto FromArea { get; set; }
    public DistrictDto FromDistrict { get; set; }
    public MandalamDto FromMandalam { get; set; }
    public PanchayatDto FromPanchayat { get; set; }
    public string Reason { get; set; }
    public string JustificationComment { get; set; }
    public DateTimeOffset SubmittedDate  { get; set; }
    public Guid SubmittedBy { get; set; }
    public DateTimeOffset? ActionDate  { get; set; }
    public Guid? ActionBy { get; set; }
    public int Status { get; set; }
    public bool IsCanApprove { get; set; } = false;
}