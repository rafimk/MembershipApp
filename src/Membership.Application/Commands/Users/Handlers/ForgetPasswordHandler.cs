using Membership.Application.Abstractions;
using Membership.Application.Events;
using Membership.Application.Exceptions.Users;
using Membership.Application.Security;
using Membership.Core.Repositories.Users;
using Membership.Shared.Abstractions.Messaging;

namespace Membership.Application.Commands.Users.Handlers;

internal sealed class ForgetPasswordHandler : ICommandHandler<ForgetPassword>
{
    private readonly IUserRepository _repository;
    private readonly IPasswordManager _passwordManager;
    private readonly IMessagePublisher _messagePublisher;

    public ForgetPasswordHandler(IUserRepository repository, IPasswordManager passwordManager,
        IMessagePublisher messagePublisher)
    {
        _repository = repository;
        _passwordManager = passwordManager;
        _messagePublisher = messagePublisher;
    }

    public async Task HandleAsync(ForgetPassword command)
    {
        var user = await _repository.GetByEmailAsync(command.Email);

        if (user is null)
        {
            throw new UserNotFoundByEmailException(command.Email);
        }
        
        var otp = _passwordManager.Generate();
        var integrationEvent = new UserCreated(user.Email, user.FullName, otp);
        await _messagePublisher.PublishAsync<UserCreated>("user-created", integrationEvent);
    }
}