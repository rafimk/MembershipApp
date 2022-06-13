using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Nationalities.Mandalams.Hanlers;

internal sealed class UpdateMandalamHandler : ICommandHandler<UpdateMandalam>
{
    private readonly IMandalamRepository _repository;

    public UpdateMandalamHandler(IMandalamRepository repository)
        => _repository = repository;

    public async Task HandleAsync(UpdateMandalam command)
    {
        var mandalam = await _repository.GetByIdAsync(command.Id);

        if (mandalam is null)
        {
            throw new MandalamNotFoundException(command.Id);
        }
        mandalam.Update(command.Name, command.DistrictId);
        await _repository.UpdateAsync(mandalam);
    }
}