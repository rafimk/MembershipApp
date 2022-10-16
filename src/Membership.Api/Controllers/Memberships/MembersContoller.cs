using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Membership.Api.Controllers.Commons;
using Membership.Application.Abstractions;
using Membership.Application.Commands.Memberships.Members;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Members;
using Membership.Application.Queries.Pagination;
using Membership.Infrastructure.FileManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Membership.Api.Controllers.Memberships;

public class MembersController : ApiController
{
    private readonly ICommandHandler<CreateMember> _createMemberHandler;
    private readonly ICommandHandler<UpdateMembershipId> _updateMembershipIdHandler;
    private readonly ICommandHandler<ActivateMember> _activateMemberHandler;
    private readonly ICommandHandler<DeactivateMember> _deactivateMemberHandler;
    private readonly IQueryHandler<GetMemberById, MemberDto> _getMemberByIdHandler;
    private readonly IQueryHandler<GetMembersByRoleAsExcel, IEnumerable<MemberListDto>> _getMembersByRoleAsExcel;
    private readonly IQueryHandler<GetMembersByRole, PaginatedResult<MemberListDto>> _getMembersByRoleHandler;
    private readonly IQueryHandler<IsDispute, IsDisputeDto> _isDisputeHandler;
    private readonly IQueryHandler<GetMembershipCard, MemberCardDto> _getMembershipCardHandler;
    private readonly IQueryHandler<GetMembershipCardPdf, ReportDto> _getMembershipCardPdfHandler;
    private readonly IBufferedFileUploadService _bufferedFileUploadService;

    public MembersController(ICommandHandler<CreateMember> createMemberHandler,
        ICommandHandler<UpdateMembershipId> updateMembershipIdHandler,
        ICommandHandler<ActivateMember> activateMemberHandler,
        ICommandHandler<DeactivateMember> deactivateMemberHandler,
        IQueryHandler<GetMembersByRoleAsExcel, IEnumerable<MemberListDto>> getMembersByRoleAsExcel,
        IQueryHandler<GetMemberById, MemberDto> getMemberByIdHandler,
        IQueryHandler<GetMembersByRole, PaginatedResult<MemberListDto>> getMembersByRoleHandler,
        IQueryHandler<IsDispute, IsDisputeDto> isDisputeHandler,
        IQueryHandler<GetMembershipCard, MemberCardDto> getMembershipCardHandler,
        IQueryHandler<GetMembershipCardPdf, ReportDto> getMembershipCardPdfHandler,
        IBufferedFileUploadService bufferedFileUploadService)
    {
        _createMemberHandler = createMemberHandler;
        _updateMembershipIdHandler = updateMembershipIdHandler;
        _activateMemberHandler = activateMemberHandler;
        _deactivateMemberHandler = deactivateMemberHandler;
        _getMembersByRoleAsExcel = getMembersByRoleAsExcel;
        _getMemberByIdHandler = getMemberByIdHandler;
        _getMembersByRoleHandler = getMembersByRoleHandler;
        _isDisputeHandler = isDisputeHandler;
        _getMembershipCardHandler = getMembershipCardHandler;
        _getMembershipCardPdfHandler = getMembershipCardPdfHandler;
        _bufferedFileUploadService = bufferedFileUploadService;
    }
    
    // [HttpGet()]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // public async Task<ActionResult<IEnumerable<MemberDto>>> Get()
    // {
    //     var members = await _getMembersHandler.HandleAsync(new GetMembers { });
    //     
    //     if (members is null)
    //     {
    //         return NotFound();
    //     }
    //
    //     return Ok(members);
    // }
    
    [Authorize(Roles = "mandalam-agent, district-agent")]
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
    
    [Authorize(Roles = "mandalam-agent, district-agent")]
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
    
    [Authorize(Roles = "mandalam-agent, district-agent")]
    [HttpPost("role")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PaginatedResult<MemberListDto>>> GetMembersByRole(GetMembersByRole query)
    {
        if (string.IsNullOrWhiteSpace(User.Identity?.Name))
        {
            return NotFound();
        }

        var userId = Guid.Parse(User?.Identity?.Name);
        query = query with { UserId = userId};
        
        var members = await _getMembersByRoleHandler.HandleAsync(query);
        
        if (members is null)
        {
            return NotFound();
        }

        return Ok(members);
    }

    [Authorize(Roles = "mandalam-agent, district-agent")]
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
        var member = await _getMemberByIdHandler.HandleAsync(new GetMemberById { MemberId = (Guid)command.Id});
        return Ok(new {Id = command.Id, MembershipId = member.MembershipId});
    }
    
    [Authorize(Roles = "mandalam-agent, district-agent")]
    [HttpPut("{memberId:guid}")]
    public async Task<ActionResult> Put(Guid memberId)
    {
        await _updateMembershipIdHandler.HandleAsync(new UpdateMembershipId { Id = memberId});
        return NoContent();
    }
    
    [Authorize(Roles = "mandalam-agent, district-agent")]
    [HttpPut("activate/{memberId:guid}")]
    public async Task<ActionResult> Activate(Guid memberId)
    {
        await _activateMemberHandler.HandleAsync( new ActivateMember { MemberId = memberId});
        return NoContent();
    }
    
    [Authorize(Roles = "mandalam-agent, district-agent")]
    [HttpPut("deactivate/{memberId:guid}")]
    public async Task<ActionResult> Deactivate(Guid memberId)
    {
        await _deactivateMemberHandler.HandleAsync( new DeactivateMember { MemberId = memberId});
        return NoContent();
    }
    
    [Authorize(Roles = "mandalam-agent, district-agent")]
    [HttpGet("membershipcard/{memberId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MemberCardDto>> GetMembershipCard(Guid memberId)
    {
        var result = await _getMembershipCardHandler.HandleAsync(new GetMembershipCard { MemberId = memberId});
        return Ok(result);
    }
    
    [Authorize(Roles = "mandalam-agent, district-agent")]
    [HttpGet("membershipcardpdf/{memberId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetMembershipCardPdf(Guid memberId)
    {
        var result = await _getMembershipCardPdfHandler.HandleAsync(new GetMembershipCardPdf { MemberId = memberId});
        return File(result.File, result.FileType, result.FileName);
    }
    
    [Authorize(Roles = "mandalam-agent, district-agent")]
    [HttpPost("role-excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetMembersByRoleAsExcel(GetMembersByRoleAsExcel query)
    {
        if (string.IsNullOrWhiteSpace(User.Identity?.Name))
        {
            return NotFound();
        }

        var userId = Guid.Parse(User?.Identity?.Name);
        query = query with { UserId = userId};
        
        var members = await _getMembersByRoleAsExcel.HandleAsync(query);
        
        if (members is null)
        {
            return NotFound();
        }
        
        var result = _bufferedFileUploadService.MembersExcelReportDownload(members);
        return File(result.File, result.FileType, result.FileName);
    }
}