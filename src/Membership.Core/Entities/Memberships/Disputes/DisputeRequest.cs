using Membership.Core.Contracts.Memberships;
using Membership.Core.Consts;
using Membership.Core.Entities.Memberships.Members;
using Membership.Core.Entities.Nationalities;
using Membership.Core.ValueObjects;

namespace Membership.Core.Entities.Memberships.Disputes;

public class DisputeRequest
{
    public GenericId Id { get; private set; }
    public GenericId MemberId { get; private set; }
    public Member Member { get; private set; }
    public GenericId ProposedAreaId { get; private set; }
    public Area ProposedArea { get; private set; }
    public GenericId ProposedMandalamId { get; private set; }
    public Mandalam ProposedMandalam { get; private set; }
    public GenericId ProposedPanchayatId { get; private set; }
    public Panchayat ProposedPanchayat { get; private set; }
    public string Reason { get; private set; }
    public string JustificationComment { get; private set; }
    public DateTime SubmittedDate  { get; private set; }
    public GenericId SubmittedBy { get; private set; }
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
        ProposedAreaId = contract.ProposedAreaId;
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
            ProposedAreaId = contract.ProposedAreaId,
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

    public void Approve(string justificationComment)
    {
        JustificationComment = justificationComment;
        Status = DisputeStatus.Approved;
    }

    public void Rejecte(string justificationComment)
    {
        JustificationComment = justificationComment;
        Status = DisputeStatus.Rejected;
    }
}