using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Nationalities.Mandalams.Hanlers;

internal sealed class DeleteMandalamHandler : ICommandHandler<DeleteMandalam>
{
    private readonly IMandalamRepository _repository;

    public DeleteMandalamHandler(IMandalamRepository repository)
        => _repository = repository;

    public async Task HandleAsync(DeleteMandalam command)
    {
        var mandalam = await _repository.GetByIdAsync(command.Id);

        if (mandalam is null)
        {
            throw new MandalamNotFoundException(command.Id);
        }
        mandalam.Delete();
        await _repository.UpdateAsync(mandalam);
    }
}