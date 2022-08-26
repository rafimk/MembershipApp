using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Membership.Api.Controllers.Commons;
using Membership.Application.Abstractions;
using Membership.Application.Commands.Nationalities.Areas;
using Membership.Application.DTO.Nationalities;
using Membership.Application.Queries.Nationalities.Areas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Membership.Api.Controllers.Nationalities;

public class AreasController : ApiController
{
    private readonly ICommandHandler<CreateArea> _createAreaHandler;
    private readonly ICommandHandler<UpdateArea> _updateAreaHaneHandler;
    private readonly ICommandHandler<DeleteArea> _deleteAreaHaneHandler;
    private readonly IQueryHandler<GetAreaById, AreaDto> _getAreaByIdHandler;
    private readonly IQueryHandler<GetAreaByStateId, IEnumerable<AreaDto>> _getAreaByStateIdHandler;
    private readonly IQueryHandler<GetAreas, IEnumerable<AreaDto>> _getAreasdHandler;

    public AreasController(ICommandHandler<CreateArea> createAreaHandler,
        ICommandHandler<UpdateArea> updateAreaHaneHandler,
        ICommandHandler<DeleteArea> deleteAreaHaneHandler,
        IQueryHandler<GetAreaById, AreaDto> getAreaByIdHandler,
        IQueryHandler<GetAreaByStateId, IEnumerable<AreaDto>> getAreaByStateIdHandler,
        IQueryHandler<GetAreas, IEnumerable<AreaDto>> getAreasdHandler)
    {
        _createAreaHandler = createAreaHandler;
        _updateAreaHaneHandler = updateAreaHaneHandler;
        _deleteAreaHaneHandler = deleteAreaHaneHandler;
        _getAreaByIdHandler = getAreaByIdHandler;
        _getAreaByStateIdHandler = getAreaByStateIdHandler;
        _getAreasdHandler = getAreasdHandler;
    }
    
    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<AreaDto>>> Get()
    {
        var areas = await _getAreasdHandler.HandleAsync(new GetAreas { });
        
        if (areas is null)
        {
            return NotFound();
        }

        return Ok(areas);
    }
    
    [HttpGet("{areaId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AreaDto>> Get(Guid areaId)
    {
        var area = await _getAreaByIdHandler.HandleAsync(new GetAreaById { AreaId = areaId});
        if (area is null)
        {
            return NotFound();
        }

        return area;
    }

    [HttpGet("state/{stateId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<AreaDto>>> GetByStateId(Guid stateId)
    {
        var areas = await _getAreaByStateIdHandler.HandleAsync(new GetAreaByStateId { StateId = stateId});
        if (areas is null)
        {
            return NotFound();
        }

        return Ok(areas);
    }

    // [HttpPost]
    // [SwaggerOperation("Create area")]
    // [ProducesResponseType(StatusCodes.Status201Created)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // public async Task<ActionResult> Post(CreateArea command)
    // {
    //     command = command with {AreaId = Guid.NewGuid()};
    //     await _createAreaHandler.HandleAsync(command);
    //     return CreatedAtAction(nameof(Get), command, null);
    // }
    //
    // [HttpPut("{areaId:guid}")]
    // public async Task<ActionResult> Put(Guid areaId, UpdateArea command)
    // {
    //     await _updateAreaHaneHandler.HandleAsync(command with {AreaId = areaId});
    //     return NoContent();
    // }
    //
    // [HttpDelete("{areaId:guid}")]
    // public async Task<ActionResult> Delete(Guid areaId)
    // {
    //     await _deleteAreaHaneHandler.HandleAsync( new DeleteArea {AreaId = areaId});
    //     return NoContent();
    // }
}