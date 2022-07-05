using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Memberships;
using Membership.Core.Repositories.Memberships;

namespace Membership.Application.Commands.Memberships.Disputes.Handlers;

internal sealed class ApproveDisputeRequestHandler : ICommandHandler<ApproveDisputeRequest>
{
    private readonly IDisputeRequestRepository _repository;

    public ApproveDisputeRequestHandler(IDisputeRequestRepository repository)
        => _repository = repository;

    public async Task HandleAsync(ApproveDisputeRequest command)
    {
        var disputeRequest = await _repository.GetByIdAsync(command.RequestId);

        if (disputeRequest is null)
        {
            throw new DisputeRequestNotFoundException(command.RequestId);
        }
        
        disputeRequest.Approve(command.JustificationComment);
    }
}