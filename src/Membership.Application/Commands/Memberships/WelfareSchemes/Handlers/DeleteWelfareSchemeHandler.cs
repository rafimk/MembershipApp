using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Memberships;
using Membership.Core.Repositories.Memberships;

namespace Membership.Application.Commands.Memberships.WelfareSchemes.Handlers;

internal sealed class DeleteWelfareSchemeHandler : ICommandHandler<DeleteWelfareScheme>
{
    private readonly IWelfareSchemeRepository _repository;

    public DeleteWelfareSchemesHandler(IWelfareSchemeRepository repository)
        => _repository = repository;

    public async Task HandleAsync(DeleteWelfareScheme command)
    {
        var welfareSchemes = await _repository.GetByIdAsync(command.WelfareSchemeId);

        if (welfareSchemes is null)
        {
            throw new WelfareSchemeNotFoundException(command.WelfareSchemeId);
        }
        welfareSchemes.Delete();
        await _repository.UpdateAsync(welfareSchemes);
    }
}