using Membership.Application.Abstractions;
using Membership.Core.Entities.Nationalities;
using Membership.Core.Repositories.Nationalities;

namespace Membership.Application.Commands.Nationalities.Mandalams.Handlers;

internal sealed class CreateMandalamHandler : ICommandHandler<CreateMandalam>
{
    private readonly IMandalamRepository _repository;

    public CreateMandalamHandler(IMandalamRepository repository)
        => _repository = repository;

    public async Task HandleAsync(CreateMandalam command)
    {
        var mandalam = Mandalam.Create(Guid.NewGuid(), command.Name, command.DistrictId, DateTime.UtcNow);
        await _repository.AddAsync(mandalam);
    }
}