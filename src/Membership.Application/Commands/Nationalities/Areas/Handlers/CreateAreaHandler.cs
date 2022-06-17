using Membership.Application.Abstractions;
using Membership.Core.Entities.Nationalities;
using Membership.Core.Repositories.Nationalities;

namespace Membership.Application.Commands.Nationalities.Areas.Handlers;

internal sealed class CreateAreaHandler : ICommandHandler<CreateArea>
{
    private readonly IAreaRepository _repository;

    public CreateAreaHandler(IAreaRepository repository)
        => _repository = repository;

    public async Task HandleAsync(CreateArea command)
    {
        var area = Area.Create(command.AreaId, command.Name, command.StateId, DateTime.UtcNow);
        await _repository.AddAsync(area);
    }
}