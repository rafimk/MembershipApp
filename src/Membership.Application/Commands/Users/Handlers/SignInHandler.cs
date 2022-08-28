using Membership.Application.Abstractions;
using Membership.Application.Exceptions;
using Membership.Application.Exceptions.Users;
using Membership.Application.Security;
using Membership.Core.Abstractions;
using Membership.Core.DomainServices.Users;
using Membership.Core.Exceptions.Users;
using Membership.Core.Repositories.Users;

namespace Membership.Application.Commands.Users.Handlers;

internal sealed class SignInHandler : ICommandHandler<SignIn>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticator _authenticator;
    private readonly IPasswordManager _passwordManager;
    private readonly ITokenStorage _tokenStorage;
    private readonly IClock _clock;
    private readonly IUserService _userService;

    public SignInHandler(IUserRepository userRepository, IAuthenticator authenticator, IPasswordManager passwordManager,
        ITokenStorage tokenStorage, IClock clock, IUserService userService)
    {
        _userRepository = userRepository;
        _authenticator = authenticator;
        _passwordManager = passwordManager;
        _tokenStorage = tokenStorage;
        _clock = clock;
        _userService = userService;
    }
    
    public async Task HandleAsync(SignIn command)
    {
        var user = await _userRepository.GetByEmailAsync(command.Email);
        
        if (user is null)
        {
            throw new InvalidCredentialsException();
        }
        
        if (!user.IsActive)
        {
            throw new YouAccountIsDisabledException();
        }

        // if (user.Locked is true)
        // {
        //     var ts = _clock.Current().Subtract(user.LastIncorrectLogin);
        //     if (ts.Minutes < 60)
        //     {
        //         throw new TooManyFailedLoginAttempts();
        //     }
        //
        //     _userService.ResetIncorrectLoginAttempts(user.Id, _clock.Current());
        // }

        if (!_passwordManager.Validate(command.Password, user.PasswordHash))
        {
            // var userId = user.Id;
            // user = null;
            // _userService.IncreaseIncorrectLoginAttempts(userId, _clock.Current());
            throw new InvalidCredentialsException();
        }

        var jwt = _authenticator.CreateToken(user.Id, user.Role);
        _tokenStorage.Set(jwt);
    }
}