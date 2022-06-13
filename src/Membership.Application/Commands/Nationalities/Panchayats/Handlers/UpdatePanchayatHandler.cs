using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Nationalities;
using Membership.Core.Consts;
using Membership.Core.Repositories.Nationalities;

namespace Membership.Application.Commands.Nationalities.Panchayats.Handlers;

internal sealed class UpdatePanchayatHandler : ICommandHandler<UpdatePanchayat>
{
    private readonly IPanchayatRepository _repository;

    public UpdatePanchayatHandler(IPanchayatRepository repository)
        => _repository = repository;

    public async Task HandleAsync(UpdatePanchayat command)
    {
        var mandalam = await _repository.GetByIdAsync(command.PanchayatId);

        if (mandalam is null)
        {
            throw new DistrictNotFoundException(command.PanchayatId);
        }
        mandalam.Update(command.Name, command.MandalamId, (PanchayatType)command.Type);
        await _repository.UpdateAsync(mandalam);
    }
}