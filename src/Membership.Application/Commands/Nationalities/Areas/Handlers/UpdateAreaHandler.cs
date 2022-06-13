using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Nationalities.Areas.Handlers;

internal sealed class UpdateAreaHandler : ICommandHandler<UpdateArea>
{
    private readonly IAreaRepository _repository;

    public UpdateAreaHandler(IAreaRepository repository)
        => _repository = repository;

    public async Task HandleAsync(UpdateArea command)
    {
        var area = await _repository.GetByIdAsync(command.Id);

        if (area is null)
        {
            throw new AreaNotFoundException(command.Id);
        }
        area.Update(command.Name, command.StateId);
        await _repository.UpdateAsync(area);
    }
}