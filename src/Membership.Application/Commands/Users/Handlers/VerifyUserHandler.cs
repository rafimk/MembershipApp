using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Users;
using Membership.Application.Security;
using Membership.Core.Abstractions;
using Membership.Core.Repositories.Users;

namespace Membership.Application.Commands.Users.Handlers;

internal sealed class VerifyUserHandler : ICommandHandler<VerifyUser>
{
    private readonly IUserRepository _repository;
    private readonly IPasswordManager _passwordManager;
    private readonly IClock _clock;

    public VerifyUserHandler(IUserRepository repository, IPasswordManager passwordManager, IClock clock)
    {
        _repository = repository;
        _passwordManager = passwordManager;
        _clock = clock;
    }

    public async Task HandleAsync(VerifyUser command)
    {
        var user = await _repository.GetByEmailAsync(command.Email);

        if (user is null)
        {
            throw new UserNotFoundByEmailException(command.Email);
        }

        var stringMobile = user.MobileNumber.Value.ToString();

        var mobileLastFourDigit = stringMobile.Substring(stringMobile.Length - 4, 4);

        if (mobileLastFourDigit != command.MobileLastFourDigit)
        {
            throw new InvalidCredentialsException();
        }
        
        if (!_passwordManager.Validate(command.VerifyCode, user.PasswordHash))
        {
            throw new InvalidCredentialsException();
        }

        if (command.Password != command.ConfirmPassword)
        {
            throw new PasswordAndConfirmPasswordMissmatchException();
        }
        
        var securedPassword = _passwordManager.Secure(command.Password);

        user.Verify(_clock.Current(), securedPassword);
        await _repository.UpdateAsync(user);
    }
}