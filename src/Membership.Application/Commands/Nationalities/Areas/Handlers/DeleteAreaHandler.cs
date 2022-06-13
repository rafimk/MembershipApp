using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Nationalities.Areas.Handlers;

internal sealed class DeleteAreaHandler : ICommandHandler<DeleteArea>
{
    private readonly IAreaRepository _repository;

    public DeleteAreaHandler(IAreaRepository repository)
        => _repository = repository;

    public async Task HandleAsync(DeleteArea command)
    {
        var area = await _repository.GetByIdAsync(command.Id);

        if (area is null)
        {
            throw new AreaNotFoundException(command.Id);
        }
        
        area.Delete();
        await _repository.UpdateAsync(area);
    }
}