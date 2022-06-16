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
public class MembershipPeriodsController : ControllerBase
{
    private readonly ICommandHandler<CreateMembershipPeriod> _createMembershipPeriodHandler;
    private readonly ICommandHandler<UpdateMembershipPeriod> _updateMembershipPeriodHaneHandler;
    private readonly ICommandHandler<DeleteMembershipPeriod> _deleteMembershipPeriodHaneHandler;
    private readonly IQueryHandler<GetMembershipPeriodById, MembershipPeriodtDto> _getMembershipPeriodByIdHandler;
    private readonly IQueryHandler<GetMembershipPeriods, IEnumerable<MembershipPeriodtDto>> _getMembershipPeriodsdHandler;

    public MembershipPeriodsController(ICommandHandler<CreateMembershipPeriod> createMembershipPeriodHandler,
        ICommandHandler<UpdateMembershipPeriod> updateMembershipPeriodHaneHandler,
        ICommandHandler<DeleteMembershipPeriod> deleteMembershipPeriodHaneHandler,
        IQueryHandler<GetMembershipPeriodById, MembershipPeriodtDto> getMembershipPeriodByIdHandler,
        IQueryHandler<GetMembershipPeriods, IEnumerable<DistrictDto>> getMembershipPeriodsdHandler)
    {
        _createMembershipPeriodHandler = createMembershipPeriodHandler;
        _updateMembershipPeriodHaneHandler = updateMembershipPeriodHaneHandler;
        _deleteMembershipPeriodHaneHandler = deleteMembershipPeriodHaneHandler;
        _getMembershipPeriodByIdHandler = getMembershipPeriodByIdHandler;
        _getMembershipPeriodsdHandler = getMembershipPeriodsdHandler;
    }
    
    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<DistrictDto>>> Get()
    {
        var MembershipPeriods = await _getMembershipPeriodsdHandler.HandleAsync(new GetDistricts { });
        
        if (MembershipPeriods is null)
        {
            return NotFound();
        }

        return Ok(MembershipPeriods);
    }
    
    [HttpGet("{MembershipPeriodId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DistrictDto>> Get(Guid MembershipPeriodId)
    {
        var MembershipPeriod = await _getMembershipPeriodByIdHandler.HandleAsync(new GetMembershipPeriodById { MembershipPeriodId = MembershipPeriodId});
        if (MembershipPeriod is null)
        {
            return NotFound();
        }

        return MembershipPeriod;
    }

    [HttpPost]
    [SwaggerOperation("Create MembershipPeriod")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(CreateMembershipPeriod command)
    {
        command = command with {Id = Guid.NewGuid()};
        await _createMembershipPeriodHandler.HandleAsync(command);
        var route = "Get";
        return CreatedAtAction(route, new {command.Id}, null);
    }
    
    [HttpPut("{MembershipPeriodId:guid}")]
    public async Task<ActionResult> Put(Guid MembershipPeriodId, UpdateMembershipPeriod command)
    {
        await _updateMembershipPeriodHaneHandler.HandleAsync(command with {MembershipPeriodId = MembershipPeriodId});
        return NoContent();
    }
    
    [HttpDelete("{MembershipPeriodId:guid}")]
    public async Task<ActionResult> Delete(Guid MembershipPeriodId)
    {
        await _deleteMembershipPeriodHaneHandler.HandleAsync( new DeleteMembershipPeriod {MembershipPeriodId = ProfessionId});
        return NoContent();
    }
}