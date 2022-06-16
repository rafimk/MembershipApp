using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Membership.Application.Abstractions;
using Membership.Application.Commands.Nationalities.States;
using Membership.Application.DTO.Nationalities;
using Membership.Application.Queries.Nationalities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Membership.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MembersController : ControllerBase
{
    private readonly ICommandHandler<CreateMember> _createMemberHandler;
    private readonly ICommandHandler<UpdateMember> _updateMemberHaneHandler;
    private readonly ICommandHandler<DeleteMember> _deleteMemberHaneHandler;
    private readonly IQueryHandler<GetMemberById, MembertDto> _getMemberByIdHandler;
    private readonly IQueryHandler<GetMembers, IEnumerable<MembertDto>> _getMembersdHandler;

    public MembersController(ICommandHandler<CreateMember> createMemberHandler,
        ICommandHandler<UpdateMember> updateMemberHaneHandler,
        ICommandHandler<DeleteMember> deleteMemberHaneHandler,
        IQueryHandler<GetMemberById, MembertDto> getMemberByIdHandler,
        IQueryHandler<GetMembers, IEnumerable<DistrictDto>> getMembersdHandler)
    {
        _createMemberHandler = createMemberHandler;
        _updateMemberHaneHandler = updateMemberHaneHandler;
        _deleteMemberHaneHandler = deleteMemberHaneHandler;
        _getMemberByIdHandler = getMemberByIdHandler;
        _getMembersdHandler = getMembersdHandler;
    }
    
    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<DistrictDto>>> Get()
    {
        var Members = await _getMembersdHandler.HandleAsync(new GetDistricts { });
        
        if (Members is null)
        {
            return NotFound();
        }

        return Ok(Members);
    }
    
    [HttpGet("{MemberId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DistrictDto>> Get(Guid MemberId)
    {
        var Member= await _getMemberByIdHandler.HandleAsync(new GetMemberById { MemberId = MemberId});
        if (Memberis null)
        {
            return NotFound();
        }

        return Member;
    }

    [HttpPost]
    [SwaggerOperation("Create Member")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(CreateMembercommand)
    {
        command = command with {Id = Guid.NewGuid()};
        await _createMemberHandler.HandleAsync(command);
        var route = "Get";
        return CreatedAtAction(route, new {command.Id}, null);
    }
    
    [HttpPut("{MemberId:guid}")]
    public async Task<ActionResult> Put(Guid MemberId, UpdateMembercommand)
    {
        await _updateMemberHaneHandler.HandleAsync(command with {MemberId = MemberId});
        return NoContent();
    }
    
    [HttpDelete("{MemberId:guid}")]
    public async Task<ActionResult> Delete(Guid MemberId)
    {
        await _deleteMemberHaneHandler.HandleAsync( new DeleteMember{MemberId = MemberId});
        return NoContent();
    }
}