using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Users;
using Membership.Core.Contracts.Users;
using Membership.Core.Repositories.Users;
using Membership.Core.ValueObjects;

namespace Membership.Application.Commands.Users.Handlers;

internal sealed class UpdateUserHandler : ICommandHandler<UpdateUser>
{
    private readonly IUserRepository _repository;

    public UpdateUserHandler(IUserRepository repository)
        => _repository = repository;

    public async Task HandleAsync(UpdateUser command)
    {
        var user = await _repository.GetByIdAsync((Guid)command.UserId);

        if (user is null)
        {
            throw new UserNotFoundException(command.UserId);
        }

        if (user.Role == UserRole.MandalamAgent() || user.Role == UserRole.DisputeCommittee())
        {
            command.IsDisputeCommittee = false;
        }

        var contract = new UpdateCreateContract
        {
            FullName = command.FullName,
            MobileNumber = command.MobileNumber,
            AlternativeContactNumber = command.MobileNumber,
            Designation = command.Designation,
        };

        user.Update(contract);
        await _repository.UpdateAsync(user);
    }
}