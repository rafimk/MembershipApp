using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Membership.Application.Abstractions;
using Membership.Application.Commands.Users;
using Membership.Application.DTO.Security;
using Membership.Application.DTO.Users;
using Membership.Application.Queries.Users;
using Membership.Application.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Membership.Api.Controllers.Users;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IQueryHandler<GetUsers, IEnumerable<UserDto>> _getUsersHandler;
    private readonly IQueryHandler<GetUsersByRole, IEnumerable<UserDto>> _getUsersByRoleHandler;
    private readonly IQueryHandler<GetUserById, UserDto> _getUserByIdHandler;
    private readonly IQueryHandler<GetApplicableUserRole, IEnumerable<string>> _getApplicableUserRoleHandler;
    private readonly ICommandHandler<SignIn> _signInHandler;
    private readonly ICommandHandler<CreateUser> _createUserHandler;
    private readonly ICommandHandler<UpdateUser> _updateUserHandler;
    private readonly ICommandHandler<ActivateUser> _activateUserHandler;
    private readonly ICommandHandler<DeactivateUser> _deactivateUserHandler;
    private readonly ICommandHandler<VerifyUser> _verifyUserHandler;
    private readonly ICommandHandler<ChangeUserPassword> _changeUserPasswordHandler;
    private readonly ITokenStorage _tokenStorage;

    public UsersController(ICommandHandler<SignIn> signInHandler,
        IQueryHandler<GetUsers, IEnumerable<UserDto>> getUsersHandler,
        IQueryHandler<GetUserById, UserDto> getUserByIdHandler,
        IQueryHandler<GetUsersByRole, IEnumerable<UserDto>> getUsersByRoleHandler,
        ICommandHandler<CreateUser> createUserHandler,
        IQueryHandler<GetApplicableUserRole, IEnumerable<string>> getApplicableUserRoleHandler,
        ICommandHandler<UpdateUser> updateUserHandler,
        ICommandHandler<ActivateUser> activateUserHandler,
        ICommandHandler<DeactivateUser> deactivateUserHandler,
        ICommandHandler<VerifyUser> verifyUserHandler,
        ICommandHandler<ChangeUserPassword> changeUserPasswordHandler,
        ITokenStorage tokenStorage)
    {
        _signInHandler = signInHandler;
        _createUserHandler = createUserHandler;
        _getUsersHandler = getUsersHandler;
        _getUsersByRoleHandler = getUsersByRoleHandler;
        _getUserByIdHandler = getUserByIdHandler;
        _getApplicableUserRoleHandler = getApplicableUserRoleHandler;
        _updateUserHandler = updateUserHandler;
        _activateUserHandler = activateUserHandler;
        _deactivateUserHandler = deactivateUserHandler;
        _verifyUserHandler = verifyUserHandler;
        _changeUserPasswordHandler = changeUserPasswordHandler;
        _tokenStorage = tokenStorage;
    }

    [HttpGet("{userId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> Get(Guid userId)
    {
        var user = await _getUserByIdHandler.HandleAsync(new GetUserById {UserId = userId});
        if (user is null)
        {
            return NotFound();
        }

        return user;
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet("me")]
    public async Task<ActionResult<UserDto>> GetMe()
    {
        if (string.IsNullOrWhiteSpace(User.Identity?.Name))
        {
            return NotFound();
        }

        var userId = Guid.Parse(User?.Identity?.Name);
        var user = await _getUserByIdHandler.HandleAsync(new GetUserById {UserId = userId});

        return user;
    }
    
    [HttpGet("role")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UserDto>> GetUsersByRole()
    {
        if (string.IsNullOrWhiteSpace(User.Identity?.Name))
        {
            return NotFound();
        }

        var userId = Guid.Parse(User?.Identity?.Name);
        var users = await _getUsersByRoleHandler.HandleAsync(new GetUsersByRole {UserId = userId});

        return Ok(users);
    }
    
    [HttpGet]
    [SwaggerOperation("Get list of users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<UserDto>>> Get()
        => Ok(await _getUsersHandler.HandleAsync(new GetUsers {}));

    [HttpGet("roles")]
    [SwaggerOperation("Get list of applicable user roles")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<string>>> GetMyApplicableUserRole()
    {
        if (string.IsNullOrWhiteSpace(User.Identity?.Name))
        {
            return NotFound();
        }

        var userId = Guid.Parse(User.Identity?.Name);
        
        return Ok(await _getApplicableUserRoleHandler.HandleAsync(new GetApplicableUserRole { UserId = userId } ));
    }

    [HttpPost]
    [SwaggerOperation("Create the user account")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(CreateUser command)
    {
        if (string.IsNullOrWhiteSpace(User.Identity?.Name))
        {
            return Unauthorized();
        }

        var userId = Guid.Parse(User.Identity?.Name);
        
        command = command with {Id = Guid.NewGuid(), LoggedUserId = userId};
        await _createUserHandler.HandleAsync(command);
        return CreatedAtAction(nameof(Get), new {command.Id}, null);
    }
    
    [HttpPost("sign-in")]
    [SwaggerOperation("Sign in the user and return the JSON Web Token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JwtDto>> Post(SignIn command)
    {
        await _signInHandler.HandleAsync(command);
        var jwt = _tokenStorage.Get();
        return jwt;
    }
    
    [HttpPut("{userId:guid}")]
    public async Task<ActionResult> Put(Guid userId, UpdateUser command)
    {
        await _updateUserHandler.HandleAsync(command with {UserId = userId});
        return NoContent();
    }
    
    [HttpPut("activate/{userId:guid}")]
    public async Task<ActionResult> Activate(Guid userId)
    {
        await _activateUserHandler.HandleAsync( new ActivateUser {UserId = userId});
        return NoContent();
    }
    
    [HttpPut("deactivate/{userId:guid}")]
    public async Task<ActionResult> Deactivate(Guid userId)
    {
        await _deactivateUserHandler.HandleAsync( new DeactivateUser {UserId = userId});
        return NoContent();
    }
    
    [HttpPut("verify")]
    public async Task<ActionResult> Verify(VerifyUser command)
    {
        if (string.IsNullOrWhiteSpace(User.Identity?.Name))
        {
            return NotFound();
        }

        var userId = Guid.Parse(User?.Identity?.Name);
        command = command with {UserId = userId};
        await _verifyUserHandler.HandleAsync(command);
        return NoContent();
    }
    
    [HttpPut("password")]
    public async Task<ActionResult> ChangeUserPassword(ChangeUserPassword command)
    {
        if (string.IsNullOrWhiteSpace(User.Identity?.Name))
        {
            return NotFound();
        }

        var userId = Guid.Parse(User?.Identity?.Name);
        command = command with {UserId = userId};
        await _changeUserPasswordHandler.HandleAsync(command);
        return NoContent();
    }
}