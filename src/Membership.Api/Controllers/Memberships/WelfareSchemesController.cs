using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Membership.Application.Abstractions;
using Membership.Application.Commands.Memberships.WelfareSchemes;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.WelfareSchemes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Membership.Api.Controllers.Memberships;

[ApiController]
[Route("[controller]")]
public class WelfareSchemesController : ControllerBase
{
    private readonly ICommandHandler<CreateWelfareScheme> _createWelfareSchemeHandler;
    private readonly ICommandHandler<UpdateWelfareScheme> _updateWelfareSchemeHaneHandler;
    private readonly ICommandHandler<DeleteWelfareScheme> _deleteWelfareSchemeHaneHandler;
    private readonly IQueryHandler<GetWelfareSchemeById, WelfareSchemeDto> _getWelfareSchemeByIdHandler;
    private readonly IQueryHandler<GetWelfareSchemes, IEnumerable<WelfareSchemeDto>> _getWelfareSchemesdHandler;

    public WelfareSchemesController(ICommandHandler<CreateWelfareScheme> createWelfareSchemeHandler,
        ICommandHandler<UpdateWelfareScheme> updateWelfareSchemeHaneHandler,
        ICommandHandler<DeleteWelfareScheme> deleteWelfareSchemeHaneHandler,
        IQueryHandler<GetWelfareSchemeById, WelfareSchemeDto> getWelfareSchemeByIdHandler,
        IQueryHandler<GetWelfareSchemes, IEnumerable<WelfareSchemeDto>> getWelfareSchemesdHandler)
    {
        _createWelfareSchemeHandler = createWelfareSchemeHandler;
        _updateWelfareSchemeHaneHandler = updateWelfareSchemeHaneHandler;
        _deleteWelfareSchemeHaneHandler = deleteWelfareSchemeHaneHandler;
        _getWelfareSchemeByIdHandler = getWelfareSchemeByIdHandler;
        _getWelfareSchemesdHandler = getWelfareSchemesdHandler;
    }
    
    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<WelfareSchemeDto>>> Get()
    {
        var WelfareSchemes = await _getWelfareSchemesdHandler.HandleAsync(new GetWelfareSchemes { });
        
        if (WelfareSchemes is null)
        {
            return NotFound();
        }

        return Ok(WelfareSchemes);
    }
    
    [HttpGet("{WelfareSchemeId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WelfareSchemeDto>> Get(Guid WelfareSchemeId)
    {
        var WelfareScheme = await _getWelfareSchemeByIdHandler.HandleAsync(new GetWelfareSchemeById { WelfareSchemeId = WelfareSchemeId});
        if (WelfareScheme is null)
        {
            return NotFound();
        }

        return WelfareScheme;
    }

    [HttpPost]
    [SwaggerOperation("Create WelfareScheme")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(CreateWelfareScheme command)
    {
        command = command with { WelfareSchemeId = Guid.NewGuid()};
        await _createWelfareSchemeHandler.HandleAsync(command);
        return CreatedAtAction(nameof(Get), command, null);
    }
    
    [HttpPut("{WelfareSchemeId:guid}")]
    public async Task<ActionResult> Put(Guid WelfareSchemeId, UpdateWelfareScheme command)
    {
        await _updateWelfareSchemeHaneHandler.HandleAsync(command with { WelfareSchemeId = WelfareSchemeId});
        return NoContent();
    }
    
    [HttpDelete("{WelfareSchemeId:guid}")]
    public async Task<ActionResult> Delete(Guid WelfareSchemeId)
    {
        await _deleteWelfareSchemeHaneHandler.HandleAsync( new DeleteWelfareScheme { WelfareSchemeId = WelfareSchemeId});
        return NoContent();
    }
}