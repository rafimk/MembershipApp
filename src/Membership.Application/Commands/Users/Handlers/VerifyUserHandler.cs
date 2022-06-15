using Membership.Application.Abstractions;
using Membership.Application.Exceptions;
using Membership.Application.Exceptions.Users;
using Membership.Core.Abstractions;
using Membership.Core.Repositories.Users;

namespace Membership.Application.Commands.Users.Handlers;

internal sealed class VerifyUserHandler : ICommandHandler<VerifyUser>
{
    private readonly IUserRepository _repository;
    private readonly IClock _clock;

    public VerifyUserHandler(IUserRepository repository, IClock clock)
    {
        _repository = repository;
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

        var verifyCode = stringMobile.Substring(stringMobile.Length - 4, 4);

        if (verifyCode != command.VerifyCode)
        {
            throw new InvalidCredentialsException();
        }

        user.Verify(_clock.Current());
        await _repository.UpdateAsync(user);
    }
}