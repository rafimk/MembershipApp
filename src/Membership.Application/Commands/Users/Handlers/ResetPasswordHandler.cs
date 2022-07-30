using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Users;
using Membership.Application.Security;
using Membership.Core.Repositories.Users;

namespace Membership.Application.Commands.Users.Handlers;

internal sealed class ResetPasswordHandler : ICommandHandler<ResetPassword>
{
    private readonly IUserRepository _repository;
    private readonly IPasswordManager _passwordManager;

    public ResetPasswordHandler(IUserRepository repository, IPasswordManager passwordManager)
    {
        _repository = repository;
        _passwordManager = passwordManager;
    }

    public async Task HandleAsync(ResetPassword command)
    {
        var user = await _repository.GetByEmailAsync(command.Email);

        if (user is null)
        {
            throw new UserNotFoundByEmailException(command.Email);
        }

        if (!_passwordManager.Validate(command.OldPassword, user.PasswordHash))
        {
            throw new InvalidCredentialsException();
        }

        if (command.Password != command.ConfirmPassword)
        {
            throw new PasswordAndConfirmPasswordMissmatchException();
        }

        var securedPassword = _passwordManager.Secure(command.Password);

        user.ChangePassword(securedPassword);
        await _repository.UpdateAsync(user);
    }
}