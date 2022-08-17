using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Users;
using Membership.Application.Messages;
using Membership.Application.Security;
using Membership.Core.Repositories.Users;
using Membership.Core.ValueObjects;
using Membership.Shared.Publishers;

namespace Membership.Application.Commands.Users.Handlers;

internal sealed class ResetPasswordHandler : ICommandHandler<ResetPassword>
{
    private readonly IUserRepository _repository;
    private readonly IPasswordManager _passwordManager;
    private readonly IMessagePublisher _messagePublisher;

    public ResetPasswordHandler(IUserRepository repository, IMessagePublisher messagePublisher,
        IPasswordManager passwordManager)
    {
        _repository = repository;
        _passwordManager = passwordManager;
        _messagePublisher = messagePublisher;
    }

    public async Task HandleAsync(ResetPassword command)
    {
        var user = await _repository.GetByIdAsync(command.UserId);

        if (user is null)
        {
            throw new UserNotFoundException(command.UserId);
        }

        var email = new Email(command.Email);
        
        var userByEmail = await _repository.GetByEmailAsync(email);

        if (userByEmail is not null)
        {
            if (userByEmail?.Id != user.Id)
            {
                throw new EmailAlreadyInUseException(command.Email);
            }
        }
        
        var firstTimePassord = _passwordManager.Generate(); 

        var securedPassword = _passwordManager.Secure(firstTimePassord);

        user.ChangePassword(securedPassword, email.Value.ToLower());
        await _repository.UpdateAsync(user);
        string messageId = Guid.NewGuid().ToString("N");
        var message = new UserCreated(user.FullName, command.Email, firstTimePassord);
        await _messagePublisher.PublishAsync("user", $"created", message, messageId);
    }
}