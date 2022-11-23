﻿using System.Threading.Tasks;
using Membership.Api.Controllers.Commons;
using Membership.Application.Abstractions;
using Membership.Application.Queries.Verifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Membership.Api.Controllers.Verifications;

public class AutoVerificationController : ApiController
{
    private readonly IQueryHandler<AutoVerification, bool> _autoVerificationHandler;
    private readonly IQueryHandler<DownloadEmiratesId, bool> _downloadEmiratesIdHandler;

    public AutoVerificationController(IQueryHandler<AutoVerification, bool> autoVerificationHandler,
        IQueryHandler<DownloadEmiratesId, bool> downloadEmiratesIdHandler)
    {
        _autoVerificationHandler = autoVerificationHandler;
        _downloadEmiratesIdHandler = downloadEmiratesIdHandler;
    }
    
    [AllowAnonymous]
    [HttpPost()]
    public async Task<ActionResult> Verify(AutoVerification query)
    {
        var members = await _autoVerificationHandler.HandleAsync(query);
    
        return Ok();
    }
    
    [AllowAnonymous]
    [HttpPost("DownloadEID")]
    public async Task<ActionResult> DownloadEID(DownloadEmiratesId query)
    {
        var members = await _downloadEmiratesIdHandler.HandleAsync(query);
    
        return Ok();
    }
}