using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Memberships;
using Membership.Core.Repositories.Memberships;

namespace Membership.Application.Commands.Memberships.Professions.Handlers;

internal sealed class UpdateRegisteredOrganizationHandler : ICommandHandler<UpdateRegisteredOrganization>
{
    private readonly IUpdateRegisteredOrganizationRepository _repository;

    public UpdateRegisteredOrganizationHandler(IUpdateRegisteredOrganizationRepository repository)
        => _repository = repository;

    public async Task HandleAsync(UpdateRegisteredOrganization command)
    {
        var registeredOrganization = await _repository.GetByIdAsync(command.RegisteredOrganizationId);

        if (registeredOrganization is null)
        {
            throw new RegisteredOrganizationNotFoundException(command.RegisteredOrganizationId);
        }
        registeredOrganization.Update(command.Name);
        await _repository.UpdateAsync(registeredOrganization);
    }
}