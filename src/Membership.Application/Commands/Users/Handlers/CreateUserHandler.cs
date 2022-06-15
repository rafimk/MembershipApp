using Membership.Application.Abstractions;
using Membership.Core.Entities.Nationalities;
using Membership.Core.Repositories.Users;

namespace Membership.Application.Commands.Users.Handlers;

internal sealed class CreateUserHandler : ICommandHandler<CreateUser>
{
    private readonly IUserRepository _repository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IMessagePublisher _messagePublisher;
    private readonly IClock _clock;

    public CreateUserHandler(IUserRepository repository, 
        IPasswordHasher<User> passwordHasher,
        IMessagePublisher messagePublisher,
        IClock clock)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
        _messagePublisher = messagePublisher;
        _clock = clock;
    }

    public async Task HandleAsync(CreateUser command)
    {
        var role = string.IsNullOrWhiteSpace(command.Role) ? Role.User() : new Role(command.Role);
        
        if (await _userRepository.GetByEmailAsync(email) is not null)
        {
            throw new EmailAlreadyInUseException(email);
        }

        var firstTimePassord = _passwordManager.Generate(); 
        var securedPassword = _passwordManager.Secure(firstTimePassord);

        var contract = new UserCreateContract
        {
            Id = command.Id,
            FullName = command.FullName,
            Email = command.Email,
            MobileNumber = command.MobileNumber,
            AlternativeContactNumber = command.AlternativeContactNumber,
            Designation = command.Designation,
            PasswordHash = securedPassword,
            Role = role,
            CascadeId = contract.CascadeId,
            CreatedAt = _clock.Now(),
            IsActive = true
        };
        var user = User.Create(contract);
        await _repository.AddAsync(user);
        var integrationEvent = new UserCreated(user.Email, user.FullName, user.PasswordHash);
        await _messagePublisher.PublishAsync("user-created", integrationEvent);
    }
}