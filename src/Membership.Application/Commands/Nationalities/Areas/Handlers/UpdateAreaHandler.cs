using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Nationalities;
using Membership.Core.Repositories.Nationalities;

namespace Membership.Application.Commands.Nationalities.Areas.Handlers;

internal sealed class UpdateAreaHandler : ICommandHandler<UpdateArea>
{
    private readonly IAreaRepository _repository;

    public UpdateAreaHandler(IAreaRepository repository)
        => _repository = repository;

    public async Task HandleAsync(UpdateArea command)
    {
        var area = await _repository.GetByIdAsync((Guid)command.AreaId);

        if (area is null)
        {
            throw new AreaNotFoundException(command.AreaId);
        }
        area.Update(command.Name, command.StateId);
        await _repository.UpdateAsync(area);
    }
}