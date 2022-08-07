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
    private readonly ICommandHandler<UpdateMember> _updateMemberHandler;
    private readonly ICommandHandler<ActivateMember> _activateMemberHandler;
    private readonly ICommandHandler<DeactivateMember> _deactivateMemberHandler;
    private readonly IQueryHandler<GetMemberById, MemberDto> _getMemberByIdHandler;
    private readonly IQueryHandler<GetMembers, IEnumerable<MemberDto>> _getMembersHandler;
    private readonly IQueryHandler<GetMembersByRole, IEnumerable<MemberDto>> _getMembersByRoleHandler;
    private readonly IQueryHandler<IsDispute, IsDisputeDto> _isDisputeHandler;
    private readonly IQueryHandler<GetMembershipCard, MemberCardDto> _getMembershipCardHandler;
    private readonly IQueryHandler<GetMembershipCardPdf, ReportDto> _getMembershipCardPdfHandler;

    public MembersController(ICommandHandler<CreateMember> createMemberHandler,
        ICommandHandler<UpdateMember> updateMemberHandler,
        ICommandHandler<ActivateMember> activateMemberHandler,
        ICommandHandler<DeactivateMember> deactivateMemberHandler,
        IQueryHandler<GetMembers, IEnumerable<MemberDto>> getMembersHandler,
        IQueryHandler<GetMemberById, MemberDto> getMemberByIdHandler,
        IQueryHandler<GetMembersByRole, IEnumerable<MemberDto>> getMembersByRoleHandler,
        IQueryHandler<IsDispute, IsDisputeDto> isDisputeHandler,
        IQueryHandler<GetMembershipCard, MemberCardDto> getMembershipCardHandler,
        IQueryHandler<GetMembershipCardPdf, ReportDto> getMembershipCardPdfHandler)
    {
        _createMemberHandler = createMemberHandler;
        _updateMemberHandler = updateMemberHandler;
        _activateMemberHandler = activateMemberHandler;
        _deactivateMemberHandler = deactivateMemberHandler;
        _getMembersHandler = getMembersHandler;
        _getMemberByIdHandler = getMemberByIdHandler;
        _getMembersByRoleHandler = getMembersByRoleHandler;
        _isDisputeHandler = isDisputeHandler;
        _getMembershipCardHandler = getMembershipCardHandler;
        _getMembershipCardPdfHandler = getMembershipCardPdfHandler;
    }
    
    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<MemberDto>>> Get()
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
    
    [HttpGet("isdispute")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IsDisputeDto>> IsDispute(string emiratesIdNumber)
    {
        var isDispute = await _isDisputeHandler.HandleAsync(new IsDispute { EmiratesIdNumber = emiratesIdNumber});
        
        if (isDispute is null)
        {
            return NotFound();
        }

        return isDispute;
    }
    
    [HttpGet("role")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetMembersByRole()
    {
        if (string.IsNullOrWhiteSpace(User.Identity?.Name))
        {
            return NotFound();
        }

        var userId = Guid.Parse(User?.Identity?.Name);
        
        var members = await _getMembersByRoleHandler.HandleAsync(new GetMembersByRole {UserId = userId});
        
        if (members is null)
        {
            return NotFound();
        }

        return Ok(members);
    }

    [HttpPost]
    [SwaggerOperation("Create Member")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(CreateMember command)
    {
        if (string.IsNullOrWhiteSpace(User.Identity?.Name))
        {
            return NotFound();
        }
        

        var userId = Guid.Parse(User?.Identity?.Name);
        command = command with {Id =  Guid.NewGuid(), AgentId = userId};
        await _createMemberHandler.HandleAsync(command);
        // return CreatedAtAction(nameof(Get), new {command.Id}, null);
        var member = await _getMemberByIdHandler.HandleAsync(new GetMemberById { MemberId = (Guid)command.Id});
        return Ok(new {Id = command.Id, MembershipId = member.MembershipId});
    }
    
    [HttpPut("{memberId:guid}")]
    public async Task<ActionResult> Put(Guid memberId, UpdateMember command)
    {
        await _updateMemberHandler.HandleAsync(command with {Id = memberId});
        return NoContent();
    }
    
    [HttpPut("activate/{memberId:guid}")]
    public async Task<ActionResult> Activate(Guid memberId)
    {
        await _activateMemberHandler.HandleAsync( new ActivateMember { MemberId = memberId});
        return NoContent();
    }
    
    [HttpPut("deactivate/{memberId:guid}")]
    public async Task<ActionResult> Deactivate(Guid memberId)
    {
        await _deactivateMemberHandler.HandleAsync( new DeactivateMember { MemberId = memberId});
        return NoContent();
    }
    
    [HttpGet("membershipcard/{memberId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MemberCardDto>> GetMembershipCard(Guid memberId)
    {
        var result = await _getMembershipCardHandler.HandleAsync(new GetMembershipCard { MemberId = memberId});
        return Ok(result);
    }
    
    [HttpGet("membershipcardpdf/{memberId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetMembershipCardPdf(Guid memberId)
    {
        var result = await _getMembershipCardPdfHandler.HandleAsync(new GetMembershipCardPdf { MemberId = memberId});
        return File(result.File, result.FileType, result.FileName);
    }
}