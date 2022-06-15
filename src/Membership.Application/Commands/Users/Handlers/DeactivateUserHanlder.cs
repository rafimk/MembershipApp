using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Users;
using Membership.Core.Repositories.Users;

namespace Membership.Application.Commands.Nationalities.Areas.Handlers;

internal sealed class DeactivateUserHanlder : ICommandHandler<DeactivateUser>
{
    private readonly IUserRepository _repository;

    public DeactivateUserHanlder(IUserRepository repository)
        => _repository = repository;

    public async Task HandleAsync(DeactivateUser command)
    {
        var user = await _repository.GetByIdAsync(command.UserId);

        if (user is null)
        {
            throw new UserNotFoundException(command.UserId);
        }

        user.Dectivate();
        await _repository.UpdateAsync(user);
    }
}