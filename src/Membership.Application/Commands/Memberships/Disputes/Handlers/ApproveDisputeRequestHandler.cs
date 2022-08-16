using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Memberships;
using Membership.Core.Contracts.Memberships;
using Membership.Core.Repositories.Memberships;

namespace Membership.Application.Commands.Memberships.Disputes.Handlers;

internal sealed class ApproveDisputeRequestHandler : ICommandHandler<ApproveDisputeRequest>
{
    private readonly IDisputeRequestRepository _repository;
    private readonly IMemberRepository _memberRepository;

    public ApproveDisputeRequestHandler(IDisputeRequestRepository repository, IMemberRepository memberRepository)
    {
        _repository = repository;
        _memberRepository = memberRepository;
    }

    public async Task HandleAsync(ApproveDisputeRequest command)
    {
        var disputeRequest = await _repository.GetByIdAsync(command.RequestId);

        if (disputeRequest is null)
        {
            throw new DisputeRequestNotFoundException(command.RequestId);
        }

        var member = await _memberRepository.GetByIdAsync(disputeRequest.MemberId);
        
        if (disputeRequest is null)
        {
            throw new MemberNotFoundException(disputeRequest.MemberId);
        }

        var disputeApprovalContract = new DisputeApprovalContract
        { 
            StateId = disputeRequest.StateId,
            ProposedAreaId = disputeRequest.ProposedAreaId,
            DistrictId = disputeRequest.DistrictId,
            ProposedMandalamId = disputeRequest.MemberId,
            ProposedPanchayatId = disputeRequest.ProposedPanchayatId
        };
        
        member.DisputeApproval(disputeApprovalContract);
        disputeRequest.Approve(command.JustificationComment, command.ActionBy);
    }
}