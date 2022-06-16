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
public class LookupsController : ControllerBase
{
    private readonly IQueryHandler<GetLookups, LookupsDto> _getLookupsHandler;

    public LookupsController(IQueryHandler<GetLookups, LookupsDto> getLookupsHandler)
    {
        _getLookupsHandler = _getLookupsHandler;
    }
    
    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LookupsDto>> Get()
    {
        var lookups = await _getLookupsHandler.HandleAsync(new GetLookups { });
        
        if (lookups is null)
        {
            return NotFound();
        }

        return Ok(lookups);
    }
}