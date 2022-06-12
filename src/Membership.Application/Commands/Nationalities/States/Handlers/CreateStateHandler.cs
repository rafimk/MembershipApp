using Membership.Application.Abstractions;
using Membership.Core.Entities.Nationalities;
using Membership.Core.Repositories.Nationalities;

namespace Membership.Application.Commands.Nationalities.States.Handlers;

internal sealed class CreateStateHandler : ICommandHandler<CreateState>
{
    private readonly IStateRepository _repository;

    public CreateStateHandler(IStateRepository repository)
        => _repository = repository;

    public async Task HandleAsync(CreateState command)
    {
        var state = State.Create(Guid.NewGuid(), command.Name, DateTime.UtcNow);
        await _repository.AddAsync(state);
    }
}