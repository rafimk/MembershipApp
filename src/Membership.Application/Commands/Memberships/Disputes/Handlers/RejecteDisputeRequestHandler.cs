using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Memberships;
using Membership.Core.Repositories.Memberships;

namespace Membership.Application.Commands.Memberships.Disputes.Handlers;

internal sealed class RejecteDisputeRequestHandler : ICommandHandler<RejectDisputeRequest>
{
    private readonly IDisputeRequestRepository _repository;

    public RejecteDisputeRequestHandler(IDisputeRequestRepository repository)
        => _repository = repository;

    public async Task HandleAsync(RejectDisputeRequest command)
    {
        var disputeRequest = await _repository.GetByIdAsync(command.RequestId);

        if (disputeRequest is null)
        {
            throw new DisputeRequestNotFoundException(command.RequestId);
        }
        
        disputeRequest.Rejecte(command.JustificationComment, command.ActionBy);
    }
}