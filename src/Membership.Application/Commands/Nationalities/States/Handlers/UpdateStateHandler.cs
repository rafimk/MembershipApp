using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Nationalities;
using Membership.Core.Repositories.Nationalities;

namespace Membership.Application.Commands.Nationalities.States.Handlers;

internal sealed class UpdateStateHandler : ICommandHandler<UpdateState>
{
    private readonly IStateRepository _repository;

    public UpdateStateHandler(IStateRepository repository)
        => _repository = repository;

    public async Task HandleAsync(UpdateState command)
    {
        var state = await _repository.GetByIdAsync(command.Id);

        if (state is null)
        {
            throw new StateNotFoundException(command.Id);
        }
        state.Update(command.Name);
        await _repository.UpdateAsync(state);
    }
}