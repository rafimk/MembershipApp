using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Users;
using Membership.Core.Repositories.Users;

namespace Membership.Application.Commands.Nationalities.Areas.Handlers;

internal sealed class ActivateUserHandler : ICommandHandler<ActivateUser>
{
    private readonly IUserRepository _repository;

    public ActivateUserHandler(IUserRepository repository)
        => _repository = repository;

    public async Task HandleAsync(ActivateUser command)
    {
        var user = await _repository.GetByIdAsync(command.UserId);

        if (user is null)
        {
            throw new UserNotFoundException(command.UserId);
        }

        user.Activate();
        await _repository.UpdateAsync(user);
    }
}