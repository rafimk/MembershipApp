using Membership.Application.Abstractions;
using Membership.Core.Abstractions;
using Membership.Core.Entities.Memberships.RegisteredOrganizations;
using Membership.Core.Entities.Memberships.WelfareSchemes;
using Membership.Core.Repositories.Memberships;

namespace Membership.Application.Commands.Memberships.WelfareSchemes.Handlers;

internal sealed class CreateWelfareSchemeHandler : ICommandHandler<CreateWelfareScheme>
{
    private readonly IWelfareSchemeRepository _repository;
    private readonly IClock _clock;

    public CreateWelfareSchemeHandler(IWelfareSchemeRepository repository, IClock clock)
    {
        _repository = repository;
        _clock = clock;
    }

    public async Task HandleAsync(CreateWelfareScheme command)
    {
        var welfareScheme = WelfareScheme.Create(command.WelfareSchemeId, command.Name, _clock.Current());
        await _repository.AddAsync(welfareScheme);
    }
}