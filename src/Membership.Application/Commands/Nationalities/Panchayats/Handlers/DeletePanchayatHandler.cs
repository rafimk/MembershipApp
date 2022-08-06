using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Nationalities;
using Membership.Core.Repositories.Nationalities;

namespace Membership.Application.Commands.Nationalities.Panchayats.Handlers;

internal sealed class DeletePanchayatHandler : ICommandHandler<DeletePanchayat>
{
    private readonly IPanchayatRepository _repository;

    public DeletePanchayatHandler(IPanchayatRepository repository)
        => _repository = repository;

    public async Task HandleAsync(DeletePanchayat command)
    {
        var panchayat = await _repository.GetByIdAsync((Guid)command.PanchayatId);

        if (panchayat is null)
        {
            throw new PanchayatNotFoundException(command.PanchayatId);
        }
        panchayat.Delete();
        await _repository.UpdateAsync(panchayat);
    }
}