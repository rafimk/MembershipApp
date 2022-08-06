using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Memberships;
using Membership.Core.Repositories.Memberships;

namespace Membership.Application.Commands.Memberships.Professions.Handlers;

internal sealed class UpdateProfessionHandler : ICommandHandler<UpdateProfession>
{
    private readonly IProfessionRepository _repository;

    public UpdateProfessionHandler(IProfessionRepository repository)
        => _repository = repository;

    public async Task HandleAsync(UpdateProfession command)
    {
        var profession = await _repository.GetByIdAsync((Guid)command.ProfessionId);

        if (profession is null)
        {
            throw new ProfessionNotFoundException(command.ProfessionId);
        }
        profession.Update(command.Name);
        await _repository.UpdateAsync(profession);
    }
}