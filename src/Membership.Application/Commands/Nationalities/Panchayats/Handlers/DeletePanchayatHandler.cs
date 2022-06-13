using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Nationalities.Panchayats.Handlers;

internal sealed class DeletePanchayatHandler : ICommandHandler<DeletePanchayat>
{
    private readonly IPanchayatRepository _repository;

    public DeletePanchayatHandler(IPanchayatRepository repository)
        => _repository = repository;

    public async Task HandleAsync(DeletePanchayat command)
    {
        var panchayat = await _repository.GetByIdAsync(command.Id);

        if (panchayat is null)
        {
            throw new PanchayatNotFoundException(command.Id);
        }
        panchayat.Delete();
        await _repository.UpdateAsync(panchayat);
    }
}