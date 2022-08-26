using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Membership.Api.Controllers.Commons;
using Membership.Application.Abstractions;
using Membership.Application.DTO.Widgets;
using Membership.Application.Queries.Widgets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Membership.Api.Controllers.Widgets;

public class WidgetsController : ApiController
{
    private readonly IQueryHandler<GetMyWidget, IEnumerable<WidgetDto>> _getMyWidgetHandler;

    public WidgetsController(IQueryHandler<GetMyWidget, IEnumerable<WidgetDto>> getMyWidgetHandler)
    {
        _getMyWidgetHandler = getMyWidgetHandler;
    }
    
    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<WidgetDto>>> Get()
    {
        if (string.IsNullOrWhiteSpace(User.Identity?.Name))
        {
            return NotFound();
        }

        var userId = Guid.Parse(User.Identity?.Name);
        
        var widgets = await _getMyWidgetHandler.HandleAsync(new GetMyWidget {UserId = userId});
        
        if (widgets is null)
        {
            return NotFound();
        }

        return Ok(widgets);
    }
    
}