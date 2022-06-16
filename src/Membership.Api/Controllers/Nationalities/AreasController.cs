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
public class AreasController : ControllerBase
{
    private readonly ICommandHandler<CreateArea> _createAreaHandler;
    private readonly ICommandHandler<UpdateArea> _updateAreaHaneHandler;
    private readonly ICommandHandler<DeleteArea> _deleteAreaHaneHandler;
    private readonly IQueryHandler<GetAreaById, AreatDto> _getAreaByIdHandler;
    private readonly IQueryHandler<GetAreaByStateId, IEnumerable<AreatDto>> _getAreaByStateIdHandler;
    private readonly IQueryHandler<GetAreas, IEnumerable<AreatDto>> _getAreasdHandler;

    public AreasController(ICommandHandler<CreateArea> createAreaHandler,
        ICommandHandler<UpdateArea> updateAreaHaneHandler,
        ICommandHandler<DeleteArea> deleteAreaHaneHandler,
        IQueryHandler<GetAreaById, AreatDto> getAreaByIdHandler,
        IQueryHandler<GetAreaByStateId, IEnumerable<AreatDto>> getAreaByStateIdHandler,
        IQueryHandler<GetAreas, IEnumerable<DistrictDto>> getAreasdHandler)
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
    public async Task<ActionResult<IEnumerable<DistrictDto>>> Get()
    {
        var areas = await _getAreasdHandler.HandleAsync(new GetDistricts { });
        
        if (areas is null)
        {
            return NotFound();
        }

        return Ok(areas);
    }
    
    [HttpGet("{areaId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DistrictDto>> Get(Guid areaId)
    {
        var area = await _getAreaByIdHandler.HandleAsync(new GetAreaById { AreaId = areaId});
        if (area is null)
        {
            return NotFound();
        }

        return area;
    }

    [HttpGet("{stateId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<DistrictDto>>> Get(Guid stateId)
    {
        var areas = await _getAreaByStateIdHandler.HandleAsync(new GetAreaByStateId { StateId = stateId});
        if (areas is null)
        {
            return NotFound();
        }

        return areas;
    }

    [HttpPost]
    [SwaggerOperation("Create area")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(CreateArea command)
    {
        command = command with {Id = Guid.NewGuid()};
        await _createAreaHandler.HandleAsync(command);
        var route = "Get";
        return CreatedAtAction(route, new {command.Id}, null);
    }
    
    [HttpPut("{areaId:guid}")]
    public async Task<ActionResult> Put(Guid areaId, UpdateArea command)
    {
        await _updateAreaHaneHandler.HandleAsync(command with {AreaId = areaId});
        return NoContent();
    }
    
    [HttpDelete("{areaId:guid}")]
    public async Task<ActionResult> Delete(Guid areaId)
    {
        await _deleteAreaHaneHandler.HandleAsync( new DeleteArea {AreaId = areaId});
        return NoContent();
    }
}