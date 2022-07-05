using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Memberships;
using Membership.Core.Contracts.Memberships;
using Membership.Core.Repositories.Memberships;

namespace Membership.Application.Commands.Memberships.Disputes.Handlers;

internal sealed class UpdateDisputeRequestHandler : ICommandHandler<UpdateDisputeRequest>
{
    private readonly IDisputeRequestRepository _repository;

    public UpdateDisputeRequestHandler(IDisputeRequestRepository repository)
        => _repository = repository;

    public async Task HandleAsync(UpdateDisputeRequest command)
    {
        var disputeRequest = await _repository.GetByIdAsync(command.Id);

        if (disputeRequest is null)
        {
            throw new DisputeRequestNotFoundException(command.Id);
        }
        
        var contract = new UpdateDisputeRequestContract
        {
            ProposedAreaId = command.ProposedAreaId,
            Reason = command.Reason,
        };

        disputeRequest.Update(contract);
        await _repository.UpdateAsync(disputeRequest);
    }
}