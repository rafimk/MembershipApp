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
    public Guid StateId { get; private set; }
    public State State { get; private set; }
    public Guid ProposedAreaId { get; private set; }
    public Area ProposedArea { get; private set; }
    public Guid DistrictId { get; private set; }
    public District District { get; private set; }
    public Guid ProposedMandalamId { get; private set; }
    public Mandalam ProposedMandalam { get; private set; }
    public Guid ProposedPanchayatId { get; private set; }
    public Panchayat ProposedPanchayat { get; private set; }
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
        StateId = contract.StateId;
        ProposedAreaId = contract.ProposedAreaId;
        DistrictId = contract.DistrictId;
        ProposedMandalamId = contract.ProposedMandalamId;
        ProposedPanchayatId = contract.ProposedPanchayatId;
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
            StateId = contract.StateId,
            ProposedAreaId = contract.ProposedAreaId,
            DistrictId = contract.DistrictId,
            ProposedMandalamId = contract.ProposedMandalamId,
            ProposedPanchayatId = contract.ProposedPanchayatId,
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
        ProposedAreaId = contract.ProposedAreaId;
        Reason = contract.Reason;
    }

    public void Approve(string justificationComment, Guid? actionBy)
    {
        JustificationComment = justificationComment;
        ActionBy = actionBy;
        Status = DisputeStatus.Approved;
    }

    public void Rejecte(string justificationComment, Guid? actionBy)
    {
        JustificationComment = justificationComment;
        ActionBy = actionBy;
        Status = DisputeStatus.Rejected;
    }
}