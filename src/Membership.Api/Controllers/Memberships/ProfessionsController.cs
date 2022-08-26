using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Membership.Api.Controllers.Commons;
using Membership.Application.Abstractions;
using Membership.Application.Commands.Memberships.Professions;
using Membership.Application.DTO.Memberships;
using Membership.Application.DTO.Nationalities;
using Membership.Application.Queries.Memberships.Professions;
using Membership.Application.Queries.Nationalities.Districts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Membership.Api.Controllers.Memberships;

public class ProfessionsController : ApiController
{
    private readonly ICommandHandler<CreateProfession> _createProfessionHandler;
    private readonly ICommandHandler<UpdateProfession> _updateProfessionHaneHandler;
    private readonly ICommandHandler<DeleteProfession> _deleteProfessionHaneHandler;
    private readonly IQueryHandler<GetProfessionById, ProfessionDto> _getProfessionByIdHandler;
    private readonly IQueryHandler<GetProfessions, IEnumerable<ProfessionDto>> _getProfessionsdHandler;

    public ProfessionsController(ICommandHandler<CreateProfession> createProfessionHandler,
        ICommandHandler<UpdateProfession> updateProfessionHaneHandler,
        ICommandHandler<DeleteProfession> deleteProfessionHaneHandler,
        IQueryHandler<GetProfessionById, ProfessionDto> getProfessionByIdHandler,
        IQueryHandler<GetProfessions, IEnumerable<ProfessionDto>> getProfessionsdHandler)
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
    public async Task<ActionResult<IEnumerable<ProfessionDto>>> Get()
    {
        var professions = await _getProfessionsdHandler.HandleAsync(new GetProfessions { });
        
        if (professions is null)
        {
            return NotFound();
        }

        return Ok(professions);
    }
    
    [HttpGet("{professionId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProfessionDto>> Get(Guid professionId)
    {
        var profession = await _getProfessionByIdHandler.HandleAsync(new GetProfessionById { ProfessionId = professionId});
        if (profession is null)
        {
            return NotFound();
        }

        return profession;
    }

    // [HttpPost]
    // [SwaggerOperation("Create Profession")]
    // [ProducesResponseType(StatusCodes.Status201Created)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // public async Task<ActionResult> Post(CreateProfession command)
    // {
    //     command = command with { ProfessionId = Guid.NewGuid()};
    //     await _createProfessionHandler.HandleAsync(command);
    //     return CreatedAtAction(nameof(Get), command, null);
    // }
    
    // [HttpPut("{professionId:guid}")]
    // public async Task<ActionResult> Put(Guid professionId, UpdateProfession command)
    // {
    //     await _updateProfessionHaneHandler.HandleAsync(command with { ProfessionId = professionId});
    //     return NoContent();
    // }
    //
    // [HttpDelete("{professionId:guid}")]
    // public async Task<ActionResult> Delete(Guid professionId)
    // {
    //     await _deleteProfessionHaneHandler.HandleAsync( new DeleteProfession { ProfessionId = professionId});
    //     return NoContent();
    // }
}