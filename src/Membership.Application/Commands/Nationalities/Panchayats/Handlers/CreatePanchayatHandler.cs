using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Nationalities.Panchayats.Handlers;

internal sealed class CreatePanchayatHandler : ICommandHandler<CreatePanchayat>
{
    private readonly IPanchayatRepository _repository;

    public CreatePanchayatHandler(IPanchayatRepository repository)
        => _repository = repository;

    public async Task HandleAsync(CreatePanchayat command)
    {
        var panchayat = new Panchayat();
        panchayat.Create(Guid.NewGuid(), command.Name, command.MandalamId, (PanchayatType)command.Type, DateTime.UtcNow);
        await _repository.AddAsync(panchayat);
    }
}