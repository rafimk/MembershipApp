using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Memberships;
using Membership.Core.Repositories.Memberships;

namespace Membership.Application.Commands.Memberships.Professions.Handlers;

internal sealed class DeleteRegisteredOrganizationHandler : ICommandHandler<DeleteRegisteredOrganization>
{
    private readonly IRegisteredOrganizationRepository _repository;

    public DeleteRegisteredOrganizationHandler(IRegisteredOrganizationRepository repository)
        => _repository = repository;

    public async Task HandleAsync(DeleteRegisteredOrganization command)
    {
        var registeredOrganization = await _repository.GetByIdAsync(command.RegisteredOrganizationId);

        if (registeredOrganization is null)
        {
            throw new RegisteredOrganizationNotFoundException(command.RegisteredOrganizationId);
        }
        registeredOrganization.Delete();
        await _repository.UpdateAsync(registeredOrganization);
    }
}