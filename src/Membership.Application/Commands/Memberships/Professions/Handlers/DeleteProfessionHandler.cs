using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Memberships;
using Membership.Core.Repositories.Memberships;

namespace Membership.Application.Commands.Memberships.Professions.Handlers;

internal sealed class DeleteProfessionHandler : ICommandHandler<DeleteProfession>
{
    private readonly IProfessionRepository _repository;

    public DeleteProfessionHandler(IProfessionRepository repository)
        => _repository = repository;

    public async Task HandleAsync(DeleteProfession command)
    {
        var profession = await _repository.GetByIdAsync(command.ProfessionId);

        if (profession is null)
        {
            throw new ProfessionNotFoundException(command.ProfessionId);
        }
        profession.Delete();
        await _repository.UpdateAsync(profession);
    }
}