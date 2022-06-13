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
public class PanchayatsController : ControllerBase
{
    private readonly ICommandHandler<CreatePanchayat> _createPanchayatHandler;
    private readonly ICommandHandler<UpdatePanchayat> _updatePanchayatHaneHandler;
    private readonly ICommandHandler<DeletePanchayat> _deletePanchayatHaneHandler;
    private readonly IQueryHandler<GetPanchayatById, PanchayatDto> _getPanchayatByIdHandler;
    private readonly IQueryHandler<GetPanchayatByMandalamId, IEnumerable<PanchayatDto>> _getPanchayatByMandalamIdHandler;
    private readonly IQueryHandler<GetPanchayats, IEnumerable<PanchayatDto>> _getPanchayatsHandler;

    public PanchayatsController(ICommandHandler<CreatePanchayat> createPanchayatHandler,
        ICommandHandler<UpdatePanchayat> updatePanchayatHaneHandler,
        ICommandHandler<DeletePanchayat> deletePanchayatHaneHandler,
        IQueryHandler<GetPanchayatById, PanchayatDto> getPanchayatByIdHandler,
        IQueryHandler<GetPanchayatByMandalamId, IEnumerable<PanchayatDto>> getPanchayatByMandalamIdHandler,
        IQueryHandler<GetPanchayats, IEnumerable<PanchayatDto>> getPanchayatsdHandler)
    {
        _createPanchayatHandler = createPanchayatHandler;
        _updatePanchayatHaneHandler = updatePanchayatHaneHandler;
        _deletePanchayatHaneHandler = deletePanchayatHaneHandler;
        _getPanchayatByIdHandler = getPanchayatByIdHandler;
        _getPanchayatByMandalamIdHandler = getPanchayatByMandalamIdHandler;
        _getPanchayatsdHandler = getPanchayatsdHandler;
    }
    
    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<PanchayatDto>>> Get()
    {
        var Panchayats = await _getPanchayatsHandler.HandleAsync(new GetPanchayats { });
        
        if (Panchayats is null)
        {
            return NotFound();
        }

        return Ok(Panchayats);
    }
    
    [HttpGet("{panchayatId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PanchayatDto>> Get(Guid panchayatId)
    {
        var panchayat = await _deletePanchayatHaneHandler.HandleAsync(new GetPanchayatById { PanchayatId = panchayatId});
        if (panchayat is null)
        {
            return NotFound();
        }

        return panchayat;
    }

    [HttpGet("{mandalamId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<PanchayatDto>>> ByMandalamId(Guid mandalamId)
    {
        var panchayats = await _getPanchayatByMandalamIdHandler.HandleAsync(new GetPanchayatByMandalamId { MandalamId = mandalamId});
        if (panchayats is null)
        {
            return NotFound();
        }

        return panchayats;
    }

    [HttpPost]
    [SwaggerOperation("Create Panchayat")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(CreatePanchayat command)
    {
        command = command with {Id = Guid.NewGuid()};
        await _createPanchayatHandler.HandleAsync(command);
        var route = "Get";
        return CreatedAtAction(route, new {command.Id}, null);
    }
    
    [HttpPut("{panchayatId:guid}")]
    public async Task<ActionResult> Put(Guid panchayatId, UpdatePanchayat command)
    {
        await _updatePanchayatHaneHandler.HandleAsync(command with {PanchayatId = panchayatId});
        return NoContent();
    }
    
    [HttpDelete("{panchayatId:guid}")]
    public async Task<ActionResult> Delete(Guid panchayatId)
    {
        await _deletePanchayatHaneHandler.HandleAsync( new DeletePanchayat {PanchayatId = panchayatId});
        return NoContent();
    }
}