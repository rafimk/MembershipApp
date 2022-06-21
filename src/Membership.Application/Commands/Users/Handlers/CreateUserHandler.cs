using Membership.Application.Abstractions;
using Membership.Application.Events;
using Membership.Application.Exceptions.Users;
using Membership.Application.Queries.Users;
using Membership.Application.Security;
using Membership.Core.Abstractions;
using Membership.Core.Contracts.Users;
using Membership.Core.Entities.Users;
using Membership.Core.Repositories.Users;
using Membership.Core.ValueObjects;

namespace Membership.Application.Commands.Users.Handlers;

internal sealed class CreateUserHandler : ICommandHandler<CreateUser>
{
    private readonly IUserRepository _repository;
    private readonly IPasswordManager _passwordManager;
    private readonly IUserService _userService;
    private readonly IQueryHandler<GetApplicableUserRole, IEnumerable<string>> _getApplicableUserRoleHandler;
    // private readonly IMessagePublisher _messagePublisher;
    private readonly IClock _clock;

    public CreateUserHandler(IUserRepository repository, IPasswordManager passwordManager,
        IQueryHandler<GetApplicableUserRole, IEnumerable<string>> getApplicableUserRoleHandler, 
        IUserService userService, IClock clock)
    {
        _repository = repository;
        _passwordManager = passwordManager;
        _userService = userService;
        _getApplicableUserRoleHandler = getApplicableUserRoleHandler;
       // _messagePublisher = messagePublisher;
        _clock = clock;
    }

    public async Task HandleAsync(CreateUser command)
    {
       // var role = string.IsNullOrWhiteSpace(command.Role) ? User.User() : new Role(command.Role);
        
        if (await _repository.GetByEmailAsync(command.Email) is not null)
        {
            throw new EmailAlreadyInUseException(command.Email);
        }

        var availableRoles = await _getApplicableUserRoleHandler.HandleAsync(new GetApplicableUserRole {UserId = command.LoggedUserId});

        if (!availableRoles.Contains(command.Role))
        {
            throw new NotAuthorizedRoleException(command.Role);
        }

        var parentUser = _repository.gGetByIdAsync(command.UserId);

        if (parentUser is null)
        {
            throw new NotAuthorizedRoleException(command.Role);
        }

        var firstTimePassord = "admin@123"; // _passwordManager.Generate(); 
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
            Role = new UserRole(command.Role),
            StateId = _userService.GetStateId(command.cascadeId, command.userId),
            CascadeId = command.CascadeId,
            CreatedAt = _clock.Current(),
        };
        
        var user = User.Create(contract);
        await _repository.AddAsync(user);
        var integrationEvent = new UserCreated(user.Email, user.FullName, user.PasswordHash);
        // await _messagePublisher.PublishAsync<UserCreated>("user-created", integrationEvent);
    }
}