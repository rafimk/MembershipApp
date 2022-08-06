using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Nationalities;
using Membership.Core.Repositories.Nationalities;

namespace Membership.Application.Commands.Nationalities.Mandalams.Handlers;

internal sealed class UpdateMandalamHandler : ICommandHandler<UpdateMandalam>
{
    private readonly IMandalamRepository _repository;

    public UpdateMandalamHandler(IMandalamRepository repository)
        => _repository = repository;

    public async Task HandleAsync(UpdateMandalam command)
    {
        var mandalam = await _repository.GetByIdAsync((Guid)command.MandalamId);

        if (mandalam is null)
        {
            throw new MandalamNotFoundException(command.MandalamId);
        }
        mandalam.Update(command.Name, command.DistrictId);
        await _repository.UpdateAsync(mandalam);
    }
}