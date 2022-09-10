using Membership.Core.Consts;

namespace Membership.Infrastructure.DAL.ReadModels;

public class DisputeRequestReadModel
{
    public Guid Id { get; set; }
    public Guid MemberId { get; set; }
    public MemberReadModel Member { get; set; }
    
    public Guid ToStateId { get; set; }
    public StateReadModel ToState { get; set; }
    public Guid ToAreaId { get; set; }
    public AreaReadModel ToArea { get; set; }
    public Guid ToDistrictId { get; set; }
    public DistrictReadModel ToDistrict { get; set; }
    public Guid ToMandalamId { get; set; }
    public MandalamReadModel ToMandalam { get; set; }
    public Guid ToPanchayatId { get; set; }
    public PanchayatReadModel ToPanchayat { get; set; }
    
    public Guid FromStateId { get; set; }
    public StateReadModel FromState { get; set; }
    public Guid FromAreaId { get; set; }
    public AreaReadModel FromArea { get; set; }
    public Guid FromDistrictId { get; set; }
    public DistrictReadModel FromDistrict { get; set; }
    public Guid FromMandalamId { get; set; }
    public MandalamReadModel FromMandalam { get; set; }
    public Guid FromPanchayatId { get; set; }
    public PanchayatReadModel FromPanchayat { get; set; }
    public string Reason { get; set; }
    public string JustificationComment { get; set; }
    public DateTime SubmittedDate  { get; set; }
    public Guid SubmittedBy { get; set; }
    public DateTime? ActionDate  { get; set; }
    public Guid? ActionBy { get; set; }
    public DisputeStatus Status { get; set; }
}