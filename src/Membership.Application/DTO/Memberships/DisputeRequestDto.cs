using Membership.Application.DTO.Nationalities;

namespace Membership.Application.DTO.Memberships;

public class DisputeRequestDto
{
    public Guid Id { get; set; }
    public Guid MemberId { get; set; }
    public MemberBasicDto Member { get; set; }
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

    public string DisputeType
    {
        get
        {
            if (FromState?.Id == ToState?.Id)
            {
                return $"With in state {FromState.Prefix}";
            }
            else if (FromState?.Id != ToState?.Id)
            {
                return $"From {FromState.Prefix} To {ToState.Prefix}";
            }
            
            return "";
        }
    }
}
