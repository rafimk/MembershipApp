using System.Threading.Tasks;
using Membership.Api.Controllers.Commons;
using Membership.Application.Abstractions;
using Membership.Application.Queries.Verifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Membership.Api.Controllers.Verifications;

public class AutoVerificationController : ApiController
{
    private readonly IQueryHandler<AutoVerification, bool> _autoVerificationHandler;

    public AutoVerificationController(IQueryHandler<AutoVerification, bool> autoVerificationHandler)
    {
        _autoVerificationHandler = autoVerificationHandler;
    }
    
    [AllowAnonymous]
    [HttpPost()]
    public async Task<ActionResult> Verify(AutoVerification query)
    {
        var members = await _autoVerificationHandler.HandleAsync(query);
    
        return Ok();
    }
}