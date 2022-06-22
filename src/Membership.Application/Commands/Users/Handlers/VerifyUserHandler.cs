using Membership.Application.Abstractions;
using Membership.Application.Exceptions;
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
        var user = await _repository.GetByIdAsync(command.UserId);

        if (user is null)
        {
            throw new UserNotFoundException(command.UserId);
        }

        var stringMobile = user.MobileNumber.ToString();

        var mobileLastFourDigit = stringMobile.Substring(stringMobile.Length - 4, 4);

        if (mobileLastFourDigit != command.MobileLastFourDigit)
        {
            throw new InvalidCredentialsException();
        }
        
        if (!_passwordManager.Validate(command.VerifyCode, user.PasswordHash))
        {
            throw new InvalidCredentialsException();
        }

        user.Verify(_clock.Current());
        await _repository.UpdateAsync(user);
    }
}