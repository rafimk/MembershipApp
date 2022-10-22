using System;
using System.Threading.Tasks;
using Membership.Api.Controllers.Commons;
using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Members;
using Membership.Infrastructure;
using Membership.Infrastructure.FileManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Membership.Api.Controllers.Memberships;

public class MemberViewController : ApiController
{
    private readonly IQueryHandler<GetMemberViewById, MemberDto> _getMemberViewByIdHandler;
    readonly IBufferedFileUploadService _bufferedFileUploadService;
    private readonly FileUploadOptions _fileUploadOptions;

    public MemberViewController(IQueryHandler<GetMemberViewById, MemberDto> getMemberViewByIdHandler,
        IBufferedFileUploadService bufferedFileUploadService, 
        IOptions<FileUploadOptions> fileOptions)
    {
        _getMemberViewByIdHandler = getMemberViewByIdHandler;
        _bufferedFileUploadService = bufferedFileUploadService;
        _fileUploadOptions = fileOptions.Value;
    }
    
    [Authorize(Roles = "member-viewer")]
    [HttpGet("{memberId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MemberDto>> Get(Guid memberId)
    {
        var member = await _getMemberViewByIdHandler.HandleAsync(new GetMemberViewById { MemberId = memberId});
        
        if (member is null)
        {
            return NotFound();
        }

        return member;
    }
    
    [AllowAnonymous]
    [HttpGet("member-view/{Id:guid}"), DisableRequestSizeLimit]
    public async Task<ActionResult> MemberView(Guid id)
    {
        var result = await _bufferedFileUploadService.Download(id, _fileUploadOptions.FilePath);
        return File(result.File, result.FileType, result.FileName);
    }
}