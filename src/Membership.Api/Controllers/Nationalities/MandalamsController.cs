using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Membership.Api.Controllers.Commons;
using Membership.Application.Abstractions;
using Membership.Application.Commands.Nationalities.Mandalams;
using Membership.Application.DTO.Nationalities;
using Membership.Application.Queries.Nationalities.Mandalams;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Membership.Api.Controllers.Nationalities;

public class MandalamsController : ApiController
{
    private readonly ICommandHandler<CreateMandalam> _createMandalamHandler;
    private readonly ICommandHandler<UpdateMandalam> _updateMandalamHaneHandler;
    private readonly ICommandHandler<DeleteMandalam> _deleteMandalamHaneHandler;
    private readonly IQueryHandler<GetMandalamById, MandalamDto> _getMandalamByIdHandler;
    private readonly IQueryHandler<GetMandalamByDistrictId, IEnumerable<MandalamDto>> _getMandalamByDistrictIdHandler;
    private readonly IQueryHandler<GetMandalams, IEnumerable<MandalamDto>> _getMandalamsHandler;

    public MandalamsController(ICommandHandler<CreateMandalam> createMandalamHandler,
        ICommandHandler<UpdateMandalam> updateMandalamHaneHandler,
        ICommandHandler<DeleteMandalam> deleteMandalamHaneHandler,
        IQueryHandler<GetMandalamById, MandalamDto> getMandalamByIdHandler,
        IQueryHandler<GetMandalamByDistrictId, IEnumerable<MandalamDto>> getMandalamByDistrictIdHandler,
        IQueryHandler<GetMandalams, IEnumerable<MandalamDto>> getMandalamsdHandler)
    {
        _createMandalamHandler = createMandalamHandler;
        _updateMandalamHaneHandler = updateMandalamHaneHandler;
        _deleteMandalamHaneHandler = deleteMandalamHaneHandler;
        _getMandalamByIdHandler = getMandalamByIdHandler;
        _getMandalamByDistrictIdHandler = getMandalamByDistrictIdHandler;
        _getMandalamsHandler = getMandalamsdHandler;
    }
    
    // [HttpGet()]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // public async Task<ActionResult<IEnumerable<MandalamDto>>> Get()
    // {
    //     var mandalams = await _getMandalamsHandler.HandleAsync(new GetMandalams { });
    //     
    //     if (mandalams is null)
    //     {
    //         return NotFound();
    //     }
    //
    //     return Ok(mandalams);
    // }
    
    [HttpGet("{mandalamId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MandalamDto>> Get(Guid mandalamId)
    {
        var mandalam = await _getMandalamByIdHandler.HandleAsync(new GetMandalamById { MandalamId = mandalamId});
        if (mandalam is null)
        {
            return NotFound();
        }

        return mandalam;
    }

    [HttpGet("district/{districtId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<MandalamDto>>> GetByDistrictId(Guid districtId)
    {
        var mandalams = await _getMandalamByDistrictIdHandler.HandleAsync(new GetMandalamByDistrictId { DistrictId = districtId});
        if (mandalams is null)
        {
            return NotFound();
        }

        return Ok(mandalams);
    }

    // [HttpPost]
    // [SwaggerOperation("Create mandalam")]
    // [ProducesResponseType(StatusCodes.Status201Created)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // public async Task<ActionResult> Post(CreateMandalam command)
    // {
    //     command = command with {MandalamId = Guid.NewGuid()};
    //     await _createMandalamHandler.HandleAsync(command);
    //     return CreatedAtAction(nameof(Get), command, null);
    // }
    //
    // [HttpPut("{mandalamId:guid}")]
    // public async Task<ActionResult> Put(Guid mandalamId, UpdateMandalam command)
    // {
    //     await _updateMandalamHaneHandler.HandleAsync(command with {MandalamId = mandalamId});
    //     return NoContent();
    // }
    //
    // [HttpDelete("{mandalamId:guid}")]
    // public async Task<ActionResult> Delete(Guid MandalamId)
    // {
    //     await _deleteMandalamHaneHandler.HandleAsync( new DeleteMandalam {MandalamId = MandalamId});
    //     return NoContent();
    // }
}