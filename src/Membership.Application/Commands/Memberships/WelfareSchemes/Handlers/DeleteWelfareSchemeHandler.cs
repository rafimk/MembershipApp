using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Memberships;
using Membership.Core.Repositories.Memberships;

namespace Membership.Application.Commands.Memberships.WelfareSchemes.Handlers;

internal sealed class DeleteWelfareSchemeHandler : ICommandHandler<DeleteWelfareScheme>
{
    private readonly IWelfareSchemeRepository _repository;

    public DeleteWelfareSchemeHandler(IWelfareSchemeRepository repository)
        => _repository = repository;

    public async Task HandleAsync(DeleteWelfareScheme command)
    {
        var welfareScheme = await _repository.GetByIdAsync((Guid)command.WelfareSchemeId);

        if (welfareScheme is null)
        {
            throw new WelfareSchemeNotFoundException(command.WelfareSchemeId);
        }
        welfareScheme.Delete();
        await _repository.UpdateAsync(welfareScheme);
    }
}