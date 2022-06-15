using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Users;
using Membership.Core.Repositories.Users;

namespace Membership.Application.Commands.Nationalities.Areas.Handlers;

internal sealed class ChangeUserPasswordHandler : ICommandHandler<ChangeUserPassword>
{
    private readonly IUserRepository _repository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IClock _clock;

    public ChangeUserPasswordHandler(IUserRepository repository, 
        IPasswordHasher<User> passwordHasher
        IClock clock)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
        _clock = clock;
    }

    public async Task HandleAsync(ChangeUserPassword command)
    {
        var user = await _repository.GetByIdAsync(command.UserId);

        if (user is null)
        {
            throw new UserNotFoundException(command.UserId);
        }

        if (!_passwordManager.Validate(command.OldPassword, user.Password))
        {
            throw new InvalidCredentialsException();
        }

        var securedPassword = _passwordManager.Secure(firstTimePassord);

        user.ChangePassword(securedPassword);
        await _repository.UpdateAsync(user);
    }
}