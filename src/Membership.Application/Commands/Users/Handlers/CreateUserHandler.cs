using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Users;
using Membership.Application.Messages;
using Membership.Application.Security;
using Membership.Core.Abstractions;
using Membership.Core.Contracts.Users;
using Membership.Core.DomainServices.Users;
using Membership.Core.Entities.Users;
using Membership.Core.Repositories.Nationalities;
using Membership.Core.Repositories.Users;
using Membership.Core.ValueObjects;
using Membership.Shared.Publishers;

namespace Membership.Application.Commands.Users.Handlers;

internal sealed class CreateUserHandler : ICommandHandler<CreateUser>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly IUserService _userService;

    private readonly IStateRepository _stateRepository;
    private readonly IDistrictRepository _districtRepository;
    private readonly IMandalamRepository _mandalamRepository;
    
    
    private readonly IMessagePublisher _messagePublisher;
    private readonly IClock _clock;

    public CreateUserHandler(IUserRepository userRepository, IPasswordManager passwordManager,
        IDistrictRepository districtRepository, IMandalamRepository mandalamRepository,
        IStateRepository stateRepository,IUserService userService, IMessagePublisher messagePublisher, IClock clock)
    {
        _userRepository = userRepository;
        _passwordManager = passwordManager;
        _userService = userService;
        _districtRepository = districtRepository;
        _mandalamRepository = mandalamRepository;
        _stateRepository = stateRepository;
        _messagePublisher = messagePublisher;
        _clock = clock;
    }

    public async Task HandleAsync(CreateUser command)
    {
       // var role = string.IsNullOrWhiteSpace(command.Role) ? User.User() : new Role(command.Role);
        
        if (await _userRepository.GetByEmailAsync(command.Email) is not null)
        {
            throw new EmailAlreadyInUseException(command.Email);
        }
        
        var parentUser = await _userRepository.GetByIdAsync(command.LoggedUserId);

        if (parentUser is null)
        {
            throw new NotAuthorizedRoleException(command.Role);
        }

        var availableRoles = await _userService.GetApplicableUserRolesAsync(parentUser.Role, command.LoggedUserId);
        
        if (!availableRoles.Contains(command.Role))
        {
            throw new NotAuthorizedRoleException(command.Role);
        }

        var stateId = await _userService.GetStateId(command.CascadeId, command.LoggedUserId);

        var cascadeName = "";

        switch (command.Role)
        {
            case "state-admin":
            {
                var state = await _stateRepository.GetByIdAsync(command.CascadeId);

                if (state is not null)
                {
                    cascadeName = state.Name;
                }
                
                break;
            }
            case "district-admin":
            {
                var district = await _districtRepository.GetByIdAsync(command.CascadeId);

                if (district is not null)
                {
                    cascadeName = district.Name;
                }
                
                break;
            }
            case "mandalam-agent":
            {
                command.IsDisputeCommittee = false;
                
                var mandalam = await _mandalamRepository.GetByIdAsync(command.CascadeId);

                if (mandalam is not null)
                {
                    cascadeName = mandalam.Name;
                }
                break;
            }
            case "dispute-committee":
            {
                command.IsDisputeCommittee = false;
                break;
            }
        }

        var firstTimePassord = _passwordManager.Generate(); // "admin@123";;
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
            StateId = stateId,
            CascadeId = command.CascadeId,
            CascadeName = cascadeName,
            IsDisputeCommittee = command.IsDisputeCommittee,
            CreatedAt = _clock.Current(),
        };
        
        var user = User.Create(contract);
        await _userRepository.AddAsync(user);
        string messageId = Guid.NewGuid().ToString("N");
        var message = new UserCreated(command.FullName, command.Email, firstTimePassord);
        await _messagePublisher.PublishAsync("user", $"created", message, messageId);
    }
}