using Membership.Application.Abstractions;
using Membership.Core.Entities.Memberships.Professions;
using Membership.Core.Repositories.Memberships;

namespace Membership.Application.Commands.Memberships.Professions.Handlers;

internal sealed class CreateProfessionHandler : ICommandHandler<CreateProfession>
{
    private readonly IProfessionRepository _repository;

    public CreateProfessionHandler(IProfessionRepository repository)
        => _repository = repository;

    public async Task HandleAsync(CreateProfession command)
    {
        var profession = Profession.Create(Guid.NewGuid(), command.Name, DateTime.UtcNow);
        await _repository.AddAsync(profession);
    }
}