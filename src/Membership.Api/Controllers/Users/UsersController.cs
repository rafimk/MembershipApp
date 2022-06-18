using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Membership.Application.Abstractions;
using Membership.Application.Commands;
using Membership.Application.Commands.Users;
using Membership.Application.DTO.Security;
using Membership.Application.DTO.Users;
using Membership.Application.Queries.Users;
using Membership.Application.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Membership.Api.Controllers.Users;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IQueryHandler<GetUsers, IEnumerable<UserDto>> _getUsersHandler;
    private readonly IQueryHandler<GetUserById, UserDto> _getUserByIdHandler;
    private readonly IQueryHandler<GetApplicableUserRole, IEnumerable<string>> _getApplicableUserRoleHandler;
    private readonly ICommandHandler<SignIn> _signInHandler;
    private readonly ICommandHandler<CreateUser> _createUserHandler;
    private readonly ITokenStorage _tokenStorage;

    public UsersController(ICommandHandler<SignIn> signInHandler,
        IQueryHandler<GetUsers, IEnumerable<UserDto>> getUsersHandler,
        IQueryHandler<GetUserById, UserDto> getUserByIdHandler,
        ICommandHandler<CreateUser> createUserHandler,
        IQueryHandler<GetApplicableUserRole, IEnumerable<string>> getApplicableUserRoleHandler,
        ITokenStorage tokenStorage)
    {
        _signInHandler = signInHandler;
        _createUserHandler = createUserHandler;
        _getUsersHandler = getUsersHandler;
        _getUserByIdHandler = getUserByIdHandler;
        _getApplicableUserRoleHandler = getApplicableUserRoleHandler;
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

    [HttpGet]
    [SwaggerOperation("Get list of all the users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Policy = "is-admin")]
    public async Task<ActionResult<IEnumerable<UserDto>>> Get([FromQuery] GetUsers query)
        => Ok(await _getUsersHandler.HandleAsync(query));

    [HttpPost]
    [SwaggerOperation("Create the user account")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(CreateUser command)
    {
        command = command with {Id = Guid.NewGuid()};
        await _createUserHandler.HandleAsync(command);
        return CreatedAtAction(nameof(Get), command, null);
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

    [HttpGet]
    [SwaggerOperation("Get list of permited user roles")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Policy = "is-admin")]
    public async Task<ActionResult<IEnumerable<UserDto>>> Get()
        => Ok(await _getApplicableUserRoleHandler.HandleAsync(new GetApplicableUserRole {}));
}