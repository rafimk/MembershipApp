using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Nationalities;
using Membership.Core.Repositories.Nationalities;

namespace Membership.Application.Commands.Nationalities.Areas.Handlers;

internal sealed class DeleteAreaHandler : ICommandHandler<DeleteArea>
{
    private readonly IAreaRepository _repository;

    public DeleteAreaHandler(IAreaRepository repository)
        => _repository = repository;

    public async Task HandleAsync(DeleteArea command)
    {
        var area = await _repository.GetByIdAsync((Guid)command.AreaId);

        if (area is null)
        {
            throw new AreaNotFoundException(command.AreaId);
        }
        
        area.Delete();
        await _repository.UpdateAsync(area);
    }
}