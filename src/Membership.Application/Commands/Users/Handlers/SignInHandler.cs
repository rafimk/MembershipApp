using Membership.Application.Abstractions;
using Membership.Application.Exceptions;
using Membership.Application.Exceptions.Users;
using Membership.Application.Security;
using Membership.Core.Abstractions;
using Membership.Core.DomainServices.Users;
using Membership.Core.Exceptions.Users;
using Membership.Core.Repositories.Memberships;
using Membership.Core.Repositories.Users;
using Membership.Core.ValueObjects;

namespace Membership.Application.Commands.Users.Handlers;

internal sealed class SignInHandler : ICommandHandler<SignIn>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticator _authenticator;
    private readonly IPasswordManager _passwordManager;
    private readonly ITokenStorage _tokenStorage;
    private readonly IClock _clock;
    private readonly IUserService _userService;
    private readonly IMembershipPeriodRepository _membershipPeriod;

    public SignInHandler(IUserRepository userRepository, IAuthenticator authenticator, IPasswordManager passwordManager,
        ITokenStorage tokenStorage, IClock clock, IUserService userService, IMembershipPeriodRepository membershipPeriod)
    {
        _userRepository = userRepository;
        _authenticator = authenticator;
        _passwordManager = passwordManager;
        _tokenStorage = tokenStorage;
        _clock = clock;
        _userService = userService;
        _membershipPeriod = membershipPeriod;
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

        var isAddMember = false;
        var isAddUser = false;
        
        var activeMembershipPeriod = await _membershipPeriod.GetActivePeriodAsync();

        if (_clock.Current() >= activeMembershipPeriod?.RegistrationStarted &&
            _clock.Current() <= activeMembershipPeriod?.RegistrationEnded)
        {
            isAddMember = true;
            isAddUser = true;
        }

        if (user.Role == UserRole.VerificationOfficer())
        {
            isAddMember = false;
            isAddUser = false;
        }

        var jwt = _authenticator.CreateToken(user.Id, user.Role, isAddMember, isAddUser);
        _tokenStorage.Set(jwt);
    }
}