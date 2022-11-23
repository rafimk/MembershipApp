using Membership.Core.Contracts.Memberships;
using Membership.Core.Consts;
using Membership.Core.Entities.Memberships.Members;
using Membership.Core.Entities.Nationalities;
using Membership.Core.ValueObjects;

namespace Membership.Core.Entities.Memberships.Disputes;

public class DisputeRequest
{
    public Guid Id { get; private set; }
    public Guid MemberId { get; private set; }
    public Member Member { get; private set; }
    
    public Guid ToStateId { get; private set; }
    public State ToState { get; private set; }
    public Guid ToAreaId { get; private set; }
    public Area ToArea { get; private set; }
    public Guid ToDistrictId { get; private set; }
    public District ToDistrict { get; private set; }
    public Guid ToMandalamId { get; private set; }
    public Mandalam ToMandalam { get; private set; }
    public Guid ToPanchayatId { get; private set; }
    public Panchayat ToPanchayat { get; private set; }
    
    public Guid FromStateId { get; private set; }
    public State FromState { get; private set; }
    public Guid FromAreaId { get; private set; }
    public Area FromArea { get; private set; }
    public Guid FromDistrictId { get; private set; }
    public District FromDistrict { get; private set; }
    public Guid FromMandalamId { get; private set; }
    public Mandalam FromMandalam { get; private set; }
    public Guid FromPanchayatId { get; private set; }
    public Panchayat FromPanchayat { get; private set; }
    public string Reason { get; private set; }
    public string JustificationComment { get; private set; }
    public DateTime SubmittedDate  { get; private set; }
    public Guid SubmittedBy { get; private set; }
    public DateTime? ActionDate  { get; private set; }
    public Guid? ActionBy { get; private set; }
    public DisputeStatus Status { get; private set; }

    public DisputeRequest()
    {
    }

    private DisputeRequest(DisputeRequestContract contract)
    {
        Id = contract.Id;
        MemberId = contract.MemberId;
        ToStateId = contract.ToStateId;
        ToAreaId = contract.ToAreaId;
        ToDistrictId = contract.ToDistrictId;
        ToMandalamId = contract.ToMandalamId;
        ToPanchayatId = contract.ToPanchayatId;
        FromStateId = contract.FromStateId;
        FromAreaId = contract.FromAreaId;
        FromDistrictId = contract.FromDistrictId;
        FromMandalamId = contract.FromMandalamId;
        FromPanchayatId = contract.FromPanchayatId;
        Reason = contract.Reason;
        JustificationComment = contract.JustificationComment;
        SubmittedDate = contract.SubmittedDate;
        SubmittedBy = contract.SubmittedBy;
        ActionDate = contract.ActionDate;
        ActionBy = contract.ActionBy;
        Status = contract.Status;
    }

    public static DisputeRequest Create(CreateDisputeRequestContract contract)
        => new(new DisputeRequestContract
        {
            Id = contract.Id,
            MemberId = contract.MemberId,
            ToStateId = contract.ToStateId,
            ToAreaId = contract.ToAreaId,
            ToDistrictId = contract.ToDistrictId,
            ToMandalamId = contract.ToMandalamId,
            ToPanchayatId = contract.ToPanchayatId,
            FromStateId = contract.FromStateId,
            FromAreaId = contract.FromAreaId,
            FromDistrictId = contract.FromDistrictId,
            FromMandalamId = contract.FromMandalamId,
            FromPanchayatId = contract.FromPanchayatId,
            Reason = contract.Reason,
            JustificationComment = null,
            SubmittedDate = contract.SubmittedDate,
            SubmittedBy = contract.SubmittedBy,
            ActionDate = null,
            ActionBy = null,
            Status = DisputeStatus.Pending
        });

    public void Update(UpdateDisputeRequestContract contract)
    {
        ToStateId = contract.ToStateId;
        ToAreaId = contract.ToAreaId;
        ToDistrictId = contract.ToDistrictId;
        ToMandalamId = contract.ToMandalamId;
        ToPanchayatId = contract.ToPanchayatId;
        Reason = contract.Reason;
    }

    public void Approve(string justificationComment, Guid? actionBy, DateTime actionDate)
    {
        JustificationComment = justificationComment;
        ActionBy = actionBy;
        Status = DisputeStatus.Approved;
        ActionDate = actionDate;
    }

    public void Rejecte(string justificationComment, Guid? actionBy, DateTime actionDate)
    {
        JustificationComment = justificationComment;
        ActionBy = actionBy;
        Status = DisputeStatus.Rejected;
        ActionDate = actionDate;
    }
}