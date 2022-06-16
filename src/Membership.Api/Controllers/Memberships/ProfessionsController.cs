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
public class ProfessionsController : ControllerBase
{
    private readonly ICommandHandler<CreateProfession> _createProfessionHandler;
    private readonly ICommandHandler<UpdateProfession> _updateProfessionHaneHandler;
    private readonly ICommandHandler<DeleteProfession> _deleteProfessionHaneHandler;
    private readonly IQueryHandler<GetProfessionById, ProfessiontDto> _getProfessionByIdHandler;
    private readonly IQueryHandler<GetProfessions, IEnumerable<ProfessiontDto>> _getProfessionsdHandler;

    public ProfessionsController(ICommandHandler<CreateProfession> createProfessionHandler,
        ICommandHandler<UpdateProfession> updateProfessionHaneHandler,
        ICommandHandler<DeleteProfession> deleteProfessionHaneHandler,
        IQueryHandler<GetProfessionById, ProfessiontDto> getProfessionByIdHandler,
        IQueryHandler<GetProfessions, IEnumerable<DistrictDto>> getProfessionsdHandler)
    {
        _createProfessionHandler = createProfessionHandler;
        _updateProfessionHaneHandler = updateProfessionHaneHandler;
        _deleteProfessionHaneHandler = deleteProfessionHaneHandler;
        _getProfessionByIdHandler = getProfessionByIdHandler;
        _getProfessionsdHandler = getProfessionsdHandler;
    }
    
    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<DistrictDto>>> Get()
    {
        var Professions = await _getProfessionsdHandler.HandleAsync(new GetDistricts { });
        
        if (Professions is null)
        {
            return NotFound();
        }

        return Ok(Professions);
    }
    
    [HttpGet("{ProfessionId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DistrictDto>> Get(Guid ProfessionId)
    {
        var Profession = await _getProfessionByIdHandler.HandleAsync(new GetProfessionById { ProfessionId = ProfessionId});
        if (Profession is null)
        {
            return NotFound();
        }

        return Profession;
    }

    [HttpPost]
    [SwaggerOperation("Create Profession")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(CreateProfession command)
    {
        command = command with {Id = Guid.NewGuid()};
        await _createProfessionHandler.HandleAsync(command);
        var route = "Get";
        return CreatedAtAction(route, new {command.Id}, null);
    }
    
    [HttpPut("{ProfessionId:guid}")]
    public async Task<ActionResult> Put(Guid ProfessionId, UpdateProfession command)
    {
        await _updateProfessionHaneHandler.HandleAsync(command with {ProfessionId = ProfessionId});
        return NoContent();
    }
    
    [HttpDelete("{ProfessionId:guid}")]
    public async Task<ActionResult> Delete(Guid ProfessionId)
    {
        await _deleteProfessionHaneHandler.HandleAsync( new DeleteProfession {ProfessionId = ProfessionId});
        return NoContent();
    }
}