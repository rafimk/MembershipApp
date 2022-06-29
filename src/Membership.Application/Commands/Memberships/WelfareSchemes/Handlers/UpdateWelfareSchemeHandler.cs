using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Memberships;
using Membership.Core.Repositories.Memberships;

namespace Membership.Application.Commands.Memberships.WelfareSchemes.Handlers;

internal sealed class UpdateWelfareSchemeHandler : ICommandHandler<UpdateWelfareScheme>
{
    private readonly IWelfareSchemeRepository _repository;

    public UpdateWelfareSchemeHandler(IWelfareSchemeRepository repository)
        => _repository = repository;

    public async Task HandleAsync(UpdateWelfareScheme command)
    {
        var welfareScheme = await _repository.GetByIdAsync(command.WelfareSchemeId);

        if (welfareScheme is null)
        {
            throw new WelfareSchemeNotFoundException(command.WelfareSchemeId);
        }
        welfareScheme.Update(command.Name);
        await _repository.UpdateAsync(welfareScheme);
    }
}