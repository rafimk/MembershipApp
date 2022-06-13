using Membership.Application.Abstractions;
using Membership.Core.Consts;
using Membership.Core.Entities.Nationalities;
using Membership.Core.Repositories.Nationalities;

namespace Membership.Application.Commands.Nationalities.Panchayats.Handlers;

internal sealed class CreatePanchayatHandler : ICommandHandler<CreatePanchayat>
{
    private readonly IPanchayatRepository _repository;

    public CreatePanchayatHandler(IPanchayatRepository repository)
        => _repository = repository;

    public async Task HandleAsync(CreatePanchayat command)
    {
        var panchayat = Panchayat.Create(Guid.NewGuid(), command.Name, command.MandalamId, (PanchayatType)command.Type, DateTime.UtcNow);
        await _repository.AddAsync(panchayat);
    }
}