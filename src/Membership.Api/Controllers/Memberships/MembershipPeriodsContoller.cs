using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Membership.Application.Abstractions;
using Membership.Application.Commands.Memberships.MembershipPeriods;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.MembershipPeriods;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Membership.Api.Controllers.Memberships;

[ApiController]
[Route("[controller]")]
public class MembershipPeriodsController : ControllerBase
{
    private readonly ICommandHandler<CreateMembershipPeriod> _createMembershipPeriodHandler;
    private readonly ICommandHandler<UpdateMembershipPeriod> _updateMembershipPeriodHaneHandler;
    private readonly ICommandHandler<DeactivateMembershipPeriod> _deactivateMembershipPeriodHaneHandler;
    private readonly IQueryHandler<GetMembershipPeriodById, MembershipPeriodDto> _getMembershipPeriodByIdHandler;
    private readonly IQueryHandler<GetMembershipPeriods, IEnumerable<MembershipPeriodDto>> _getMembershipPeriodsdHandler;

    public MembershipPeriodsController(ICommandHandler<CreateMembershipPeriod> createMembershipPeriodHandler,
        ICommandHandler<UpdateMembershipPeriod> updateMembershipPeriodHaneHandler,
        ICommandHandler<DeactivateMembershipPeriod> deactivateMembershipPeriodHaneHandler,
        IQueryHandler<GetMembershipPeriodById, MembershipPeriodDto> getMembershipPeriodByIdHandler,
        IQueryHandler<GetMembershipPeriods, IEnumerable<MembershipPeriodDto>> getMembershipPeriodsdHandler)
    {
        _createMembershipPeriodHandler = createMembershipPeriodHandler;
        _updateMembershipPeriodHaneHandler = updateMembershipPeriodHaneHandler;
        _deactivateMembershipPeriodHaneHandler = deactivateMembershipPeriodHaneHandler;
        _getMembershipPeriodByIdHandler = getMembershipPeriodByIdHandler;
        _getMembershipPeriodsdHandler = getMembershipPeriodsdHandler;
    }
    
    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<MembershipPeriodDto>>> Get()
    {
        var membershipPeriods = await _getMembershipPeriodsdHandler.HandleAsync(new GetMembershipPeriods { });
        
        if (membershipPeriods is null)
        {
            return NotFound();
        }

        return Ok(membershipPeriods);
    }
    
    [HttpGet("{membershipPeriodId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MembershipPeriodDto>> Get(Guid membershipPeriodId)
    {
        var membershipPeriod = await _getMembershipPeriodByIdHandler.HandleAsync(new GetMembershipPeriodById { MembershipPeriodId = membershipPeriodId});
        if (membershipPeriod is null)
        {
            return NotFound();
        }

        return membershipPeriod;
    }

    [HttpPost]
    [SwaggerOperation("Create MembershipPeriod")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(CreateMembershipPeriod command)
    {
        command = command with {MembershipPeriodId = Guid.NewGuid()};
        await _createMembershipPeriodHandler.HandleAsync(command);
        return CreatedAtAction(nameof(Get),  command, null);
    }
    
    [HttpPut("{membershipPeriodId:guid}")]
    public async Task<ActionResult> Put(Guid membershipPeriodId, UpdateMembershipPeriod command)
    {
        await _updateMembershipPeriodHaneHandler.HandleAsync(command with {MembershipPeriodId = membershipPeriodId});
        return NoContent();
    }
    
    [HttpDelete("{membershipPeriodId:guid}")]
    public async Task<ActionResult> Delete(Guid membershipPeriodId)
    {
        await _deactivateMembershipPeriodHaneHandler.HandleAsync( new DeactivateMembershipPeriod {MembershipPeriodId = membershipPeriodId});
        return NoContent();
    }
}