using System;
using System.Threading.Tasks;
using Membership.Application.Abstractions;
using Membership.Application.DTO.Commons;
using Membership.Application.Queries.Commons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Membership.Api.Controllers.Commons;

public class LookupsController : ApiController
{
    private readonly IQueryHandler<GetMyLookups, MyLookupsDto> _getMyLookups;
    

    public LookupsController(IQueryHandler<GetMyLookups, MyLookupsDto> getMyLookups)
    {
        _getMyLookups = getMyLookups;
    }
    
    [HttpGet("me")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MyLookupsDto>> MyLookups()
    {
        if (string.IsNullOrWhiteSpace(User.Identity?.Name))
        {
            return NotFound();
        }

        var userId = Guid.Parse(User.Identity?.Name);
        
        var lookups = await _getMyLookups.HandleAsync(new GetMyLookups { UserId = userId});
        
        if (lookups is null)
        {
            return NotFound();
        }

        return Ok(lookups);
    }
}