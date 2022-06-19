using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Membership.Application.Abstractions;
using Membership.Application.Commands.Memberships.Members;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Members;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Membership.Api.Controllers.Memberships;

[ApiController]
[Route("[controller]")]
public class MembersController : ControllerBase
{
    private readonly ICommandHandler<CreateMember> _createMemberHandler;
    private readonly ICommandHandler<UpdateMember> _updateMemberHaneHandler;
    private readonly ICommandHandler<ActivateMember> _activateMemberHaneHandler;
    private readonly ICommandHandler<DeactivateMember> _deactivateMemberHaneHandler;
    private readonly IQueryHandler<GetMemberById, MemberDto> _getMemberByIdHandler;
    private readonly IQueryHandler<GetMembers, IEnumerable<MemberDto>> _getMembersHandler;

    public MembersController(ICommandHandler<CreateMember> createMemberHandler,
        ICommandHandler<UpdateMember> updateMemberHaneHandler,
        ICommandHandler<ActivateMember> activateMemberHaneHandler,
        ICommandHandler<DeactivateMember> deactivateMemberHaneHandler,
        IQueryHandler<GetMembers, IEnumerable<MemberDto>> getMembersHandler,
        IQueryHandler<GetMemberById, MemberDto> getMemberByIdHandler)
    {
        _createMemberHandler = createMemberHandler;
        _updateMemberHaneHandler = updateMemberHaneHandler;
        _activateMemberHaneHandler = activateMemberHaneHandler;
        _deactivateMemberHaneHandler = deactivateMemberHaneHandler;
        _getMembersHandler = getMembersHandler;
        _getMemberByIdHandler = getMemberByIdHandler;
    }
    
    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MemberDto>> Get()
    {
        var members = await _getMembersHandler.HandleAsync(new GetMembers { });
        
        if (members is null)
        {
            return NotFound();
        }

        return Ok(members);
    }
    
    [HttpGet("{memberId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MemberDto>> Get(Guid memberId)
    {
        var member = await _getMemberByIdHandler.HandleAsync(new GetMemberById { MemberId = memberId});
        
        if (member is null)
        {
            return NotFound();
        }

        return member;
    }

    [HttpPost]
    [SwaggerOperation("Create Member")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(CreateMember command)
    {
        command = command with {Id =  Guid.NewGuid()};
        await _createMemberHandler.HandleAsync(command);
        return CreatedAtAction(nameof(Get), new {command.Id}, null);
    }
    
    [HttpPut("{memberId:guid}")]
    public async Task<ActionResult> Put(Guid memberId, UpdateMember command)
    {
        await _updateMemberHaneHandler.HandleAsync(command with {Id = memberId});
        return NoContent();
    }
    
    [HttpPut("activate/{memberId:guid}")]
    public async Task<ActionResult> Activate(Guid memberId)
    {
        var command = new ActivateMember
        {
            MemberId = memberId
        };
        await _activateMemberHaneHandler.HandleAsync( new ActivateMember { MemberId = memberId});
        return NoContent();
    }
    
    [HttpPut("deactivate/{memberId:guid}")]
    public async Task<ActionResult> Deactivate(Guid memberId)
    {
        var command = new ActivateMember
        {
            MemberId = memberId
        };
        await _deactivateMemberHaneHandler.HandleAsync( new DeactivateMember { MemberId = memberId});
        return NoContent();
    }
}