using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Membership.Application.Abstractions;
using Membership.Application.Commands.Nationalities.Districts;
using Membership.Application.DTO.Nationalities;
using Membership.Application.Queries.Nationalities.Districts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Membership.Api.Controllers.Nationalities;

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
        var districts = await _getDistrictsdHandler.HandleAsync(new GetDistricts { });
        
        if (districts is null)
        {
            return NotFound();
        }

        return Ok(districts);
    }
    
    [HttpGet("{districtId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DistrictDto>> Get(Guid districtId)
    {
        var districts = await _getDistrictByIdHandler.HandleAsync(new GetDistrictById { DistrictId = districtId});
        if (districts is null)
        {
            return NotFound();
        }

        return districts;
    }

    [HttpPost]
    [SwaggerOperation("Create district")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(CreateDistrict command)
    {
        command = command with {DistrictId = Guid.NewGuid()};
        await _createDistrictHandler.HandleAsync(command);
        return CreatedAtAction(nameof(Get), command, null);
    }
    
    [HttpPut("{districtId:guid}")]
    public async Task<ActionResult> Put(Guid districtId, UpdateDistrict command)
    {
        await _updateDistrictHaneHandler.HandleAsync(command with {DistrictId = districtId});
        return NoContent();
    }
    
    [HttpDelete("{districtId:guid}")]
    public async Task<ActionResult> Delete(Guid districtId)
    {
        await _deleteDistrictHaneHandler.HandleAsync( new DeleteDistrict {DistrictId = districtId});
        return NoContent();
    }
}