using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Nationalities;
using Membership.Core.Repositories.Nationalities;

namespace Membership.Application.Commands.Nationalities.Mandalams.Handlers;

internal sealed class DeleteMandalamHandler : ICommandHandler<DeleteMandalam>
{
    private readonly IMandalamRepository _repository;

    public DeleteMandalamHandler(IMandalamRepository repository)
        => _repository = repository;

    public async Task HandleAsync(DeleteMandalam command)
    {
        var mandalam = await _repository.GetByIdAsync(command.MandalamId);

        if (mandalam is null)
        {
            throw new MandalamNotFoundException(command.MandalamId);
        }
        mandalam.Delete();
        await _repository.UpdateAsync(mandalam);
    }
}