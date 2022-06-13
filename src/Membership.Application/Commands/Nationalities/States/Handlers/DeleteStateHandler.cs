using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Nationalities;
using Membership.Core.Repositories.Nationalities;

namespace Membership.Application.Commands.Nationalities.States.Handlers;

internal sealed class DeleteStateHandler : ICommandHandler<DeleteState>
{
    private readonly IStateRepository _repository;

    public DeleteStateHandler(IStateRepository repository)
        => _repository = repository;

    public async Task HandleAsync(DeleteState command)
    {
        var state = await _repository.GetByIdAsync(command.StateId);

        if (state is null)
        {
            throw new StateNotFoundException(command.StateId);
        }
        state.Delete();
        await _repository.UpdateAsync(state);
    }
}