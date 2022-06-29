using Membership.Application.Abstractions;
using Membership.Core.Entities.Memberships.Qualifications;
using Membership.Core.Repositories.Memberships;

namespace Membership.Application.Commands.Memberships.WelfareSchemes.Handlers;

internal sealed class CreateRegisteredOrganizationHandler : ICommandHandler<CreateRegisteredOrganization>
{
    private readonly IRegisteredOrganizationRepository _repository;
    private readonly IClock _clock;

    public CreateRegisteredOrganizationHandler(IRegisteredOrganizationRepository repository, IClock clock)
    {
        _repository = repository;
        _clock = clock;
    }

    public async Task HandleAsync(CreateRegisteredOrganization command)
    {
        var registeredOrganization = RegisteredOrganization.Create(command.RegisteredOrganizationId, command.Name, _clock.Current());
        await _repository.AddAsync(qualification);
    }
}