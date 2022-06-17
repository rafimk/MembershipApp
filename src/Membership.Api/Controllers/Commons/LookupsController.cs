using System.Threading.Tasks;
using Membership.Application.Abstractions;
using Membership.Application.DTO.Commons;
using Membership.Application.Queries.Commons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Membership.Api.Controllers.Commons;

[ApiController]
[Route("[controller]")]
public class LookupsController : ControllerBase
{
    private readonly IQueryHandler<GetLookups, LookupsDto> _getLookupsHandler;

    public LookupsController(IQueryHandler<GetLookups, LookupsDto> getLookupsHandler)
    {
        _getLookupsHandler = getLookupsHandler;
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