using System;
using System.Threading.Tasks;
using Membership.Api.Controllers.Commons;
using Membership.Application.Abstractions;
using Membership.Application.Commands.Memberships.Members;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Members;
using Membership.Core.Repositories.Memberships;
using Membership.Infrastructure;
using Membership.Infrastructure.FileManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace Membership.Api.Controllers.Memberships;

public class MembershipVerificationController : ApiController
{
    private readonly ICommandHandler<ReserveVerification> _reserveVerificationHandler;
    private readonly ICommandHandler<UpdateVerification> _updateVerificationHandler;
    private readonly IQueryHandler<GetMembershipVerificationByUserId, MembershipVerificationDto> _getMembershipVerificationByUserIdHandler;
    private readonly IQueryHandler<GetReservedMembershipVerificationByUserId, Guid?> _getReservedMembershipVerificationByUserIdHandler;
    readonly IBufferedFileUploadService _bufferedFileUploadService;
    private readonly FileUploadOptions _fileUploadOptions;
    private readonly IMembershipVerificationRepository _membershipVerificationRepository;

    public MembershipVerificationController(ICommandHandler<ReserveVerification> reserveVerificationHandler,
        ICommandHandler<UpdateVerification> updateVerificationHandler, 
        IQueryHandler<GetMembershipVerificationByUserId, MembershipVerificationDto> getMembershipVerificationByUserIdHandler,
        IQueryHandler<GetReservedMembershipVerificationByUserId, Guid?> getReservedMembershipVerificationByUserIdHandler,
        IBufferedFileUploadService bufferedFileUploadService, 
        IOptions<FileUploadOptions> fileOptions,
        IMembershipVerificationRepository membershipVerificationRepository)
    {
        _reserveVerificationHandler = reserveVerificationHandler;
        _updateVerificationHandler = updateVerificationHandler;
        _getMembershipVerificationByUserIdHandler = getMembershipVerificationByUserIdHandler;
        _getReservedMembershipVerificationByUserIdHandler = getReservedMembershipVerificationByUserIdHandler;
        _bufferedFileUploadService = bufferedFileUploadService;
        _fileUploadOptions = fileOptions.Value;
        _membershipVerificationRepository = membershipVerificationRepository;
    }
    
    [Authorize(Roles = "verification-officer")]
    [HttpGet("{Id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MembershipVerificationDto>> Get(Guid id)
    {
        var userId = Guid.Parse(User?.Identity?.Name);
        
        var verification  = await _getMembershipVerificationByUserIdHandler.HandleAsync(new GetMembershipVerificationByUserId { Id = id, UserId = userId});
        
        if (verification is null)
        {
            return NotFound();
        }

        return verification;
    }
    
    [Authorize(Roles = "verification-officer")]
    [HttpPost]
    [SwaggerOperation("ReserveVerification")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(ReserveVerification command)
    {
        var userId = Guid.Parse(User?.Identity?.Name);

        var result = await _getReservedMembershipVerificationByUserIdHandler.HandleAsync(new GetReservedMembershipVerificationByUserId
            {UserId = userId});

        if (result is not null)
        {
            return Ok(new {Id = result});
        }
        
        command = command with {Id =  Guid.NewGuid(), VerifiedUserId = userId};
        await _reserveVerificationHandler.HandleAsync(command);
        return Ok(new {Id = command.Id});
    }
    
    [Authorize(Roles = "verification-officer")]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Put(Guid id, UpdateVerification command)
    {
        await _updateVerificationHandler.HandleAsync(command with {Id = id});
        return NoContent();
    }
    
    [AllowAnonymous]
    // [Authorize(Roles = "verification-officer")]
    [HttpGet("eidfrontpagedownload/{Id:guid}"), DisableRequestSizeLimit]
    public async Task<ActionResult> EidFrontPageDownload(Guid id)
    {
        var verifyRecord = await _membershipVerificationRepository.GetByIdAsync(id);
        
        if (verifyRecord is null)
        {
            return NotFound();
        }
        
        if (verifyRecord.MemberVerified)
        {
            return NotFound();
        }
        
        var result = await _bufferedFileUploadService.Download(verifyRecord.EidFrontPage, _fileUploadOptions.FilePath);
        return File(result.File, result.FileType, result.FileName);
    }
    
    [AllowAnonymous]
    // [Authorize(Roles = "verification-officer")]
    [HttpGet("eidlastpagedownload/{Id:guid}"), DisableRequestSizeLimit]
    public async Task<ActionResult> EidLastPageDownload(Guid id)
    {
        var verifyRecord = await _membershipVerificationRepository.GetByIdAsync(id);
        
        if (verifyRecord is null)
        {
            return NotFound();
        }
        
        if (verifyRecord.MemberVerified)
        {
            return NotFound();
        }
        
        var result = await _bufferedFileUploadService.Download(verifyRecord.EidLastPage, _fileUploadOptions.FilePath);
        return File(result.File, result.FileType, result.FileName);
    }
    
    [AllowAnonymous]
    // [Authorize(Roles = "verification-officer")]
    [HttpGet("passportfirstpagedownload/{Id:guid}"), DisableRequestSizeLimit]
    public async Task<ActionResult> PassportFirstPageDownload(Guid id)
    {
        var verifyRecord = await _membershipVerificationRepository.GetByIdAsync(id);
        
        if (verifyRecord is null)
        {
            return NotFound();
        }
        
        if (verifyRecord.MemberVerified)
        {
            return NotFound();
        }
        
        var result = await _bufferedFileUploadService.Download((Guid)verifyRecord.PassportFirstPage, _fileUploadOptions.FilePath);
        return File(result.File, result.FileType, result.FileName);
    }
    
    [AllowAnonymous]
    // [Authorize(Roles = "verification-officer")]
    [HttpGet("passportlastpagedownload/{Id:guid}"), DisableRequestSizeLimit]
    public async Task<ActionResult> PassportLastPageDownload(Guid id)
    {
        var verifyRecord = await _membershipVerificationRepository.GetByIdAsync(id);
        
        if (verifyRecord is null)
        {
            return NotFound();
        }
        
        if (verifyRecord.MemberVerified)
        {
            return NotFound();
        }
        
        var result = await _bufferedFileUploadService.Download((Guid)verifyRecord.PassportLastPage, _fileUploadOptions.FilePath);
        return File(result.File, result.FileType, result.FileName);
    }
}