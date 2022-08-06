using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Memberships;
using Membership.Core.Repositories.Memberships;

namespace Membership.Application.Commands.Memberships.RegisteredOrganizations.Handlers;

internal sealed class UpdateRegisteredOrganizationHandler : ICommandHandler<UpdateRegisteredOrganization>
{
    private readonly IRegisteredOrganizationRepository _repository;

    public UpdateRegisteredOrganizationHandler(IRegisteredOrganizationRepository repository)
        => _repository = repository;

    public async Task HandleAsync(UpdateRegisteredOrganization command)
    {
        var registeredOrganization = await _repository.GetByIdAsync((Guid)command.RegisteredOrganizationId);

        if (registeredOrganization is null)
        {
            throw new RegisteredOrganizationNotFoundException(command.RegisteredOrganizationId);
        }
        registeredOrganization.Update(command.Name);
        await _repository.UpdateAsync(registeredOrganization);
    }
}