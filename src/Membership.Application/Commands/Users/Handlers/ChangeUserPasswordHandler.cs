using Membership.Application.Abstractions;
using Membership.Application.Exceptions;
using Membership.Application.Exceptions.Users;
using Membership.Application.Security;
using Membership.Core.Abstractions;
using Membership.Core.Repositories.Users;

namespace Membership.Application.Commands.Users.Handlers;

internal sealed class ChangeUserPasswordHandler : ICommandHandler<ChangeUserPassword>
{
    private readonly IUserRepository _repository;
    private readonly IPasswordManager _passwordManager;
    private readonly IClock _clock;

    public ChangeUserPasswordHandler(IUserRepository repository, 
        IPasswordManager passwordManager, IClock clock)
    {
        _repository = repository;
        _passwordManager = passwordManager;
        _clock = clock;
    }

    public async Task HandleAsync(ChangeUserPassword command)
    {
        var user = await _repository.GetByIdAsync(command.UserId);

        if (user is null)
        {
            throw new UserNotFoundException(command.UserId);
        }

        if (!_passwordManager.Validate(command.OldPassword, user.PasswordHash))
        {
            throw new InvalidCredentialsException();
        }

        var securedPassword = _passwordManager.Secure(command.NewPassword);

        user.ChangePassword(securedPassword);
        await _repository.UpdateAsync(user);
    }
}