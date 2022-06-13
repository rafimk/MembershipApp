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
public class DistrictsController : ControllerBase
{
    private readonly ICommandHandler<CreateDistrict> _createDistrictHandler;
    private readonly ICommandHandler<UpdateDistrict> _updateDistrictHaneHandler;
    private readonly ICommandHandler<DeleteDistrict> _deleteDistrictHaneHandler;
    private readonly IQueryHandler<GetDistrictById, DistrictDto> _getDistrictByIdHandler;
    private readonly IQueryHandler<GetDistricts, IEnumerable<DistrictDto>> _getDistrictsdHandler;

    public DistrictsController(ICommandHandler<CreateDistrict> createDistrictHandler,
        ICommandHandler<UpdateDistrict> updateDistrictHaneHandler,
        ICommandHandler<DeleteDistrict> deleteDistrictHaneHandler,
        IQueryHandler<GetDistrictById, DistrictDto> getDistrictByIdHandler,
        IQueryHandler<GetDistricts, IEnumerable<DistrictDto>> getDistrictsdHandler)
    {
        _createDistrictHandler = createDistrictHandler;
        _updateDistrictHaneHandler = updateDistrictHaneHandler;
        _deleteDistrictHaneHandler = deleteDistrictHaneHandler;
        _getDistrictByIdHandler = getDistrictByIdHandler;
        _getDistrictsdHandler = getDistrictsdHandler;
    }
    
    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<DistrictDto>>> Get()
    {
        var states = await _getDistrictsdHandler.HandleAsync(new GetDistricts { });
        
        if (states is null)
        {
            return NotFound();
        }

        return Ok(states);
    }
    
    [HttpGet("{districtId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DistrictDto>> Get(Guid districtId)
    {
        var state = await _getDistrictByIdHandler.HandleAsync(new GetDistrictById { DistrictId = districtId});
        if (state is null)
        {
            return NotFound();
        }

        return state;
    }

    [HttpPost]
    [SwaggerOperation("Create distric")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(CreateDistrict command)
    {
        command = command with {Id = Guid.NewGuid()};
        await _createDistrictHandler.HandleAsync(command);
        var route = "Get";
        return CreatedAtAction(route, new {command.Id}, null);
    }
    
    [HttpPut("{districtId:guid}")]
    public async Task<ActionResult> Put(Guid districtId, UpdateDistrict command)
    {
        await _updateStateHaneHandler.HandleAsync(command with {DistrictId = districtId});
        return NoContent();
    }
    
    [HttpDelete("{districtId:guid}")]
    public async Task<ActionResult> Delete(Guid districtId)
    {
        await _deleteDistrictHaneHandler.HandleAsync( new DeleteDistrict {DistrictId = districtId});
        return NoContent();
    }
}