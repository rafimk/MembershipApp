using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Membership.Application.Abstractions;
using Membership.Application.Commands.Nationalities.States;
using Membership.Application.DTO;
using Membership.Application.DTO.Nationalities;
using Membership.Application.Queries.Nationalities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Membership.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class StateController : ControllerBase
{
    private readonly ICommandHandler<CreateState> _createStateHandler;
    private readonly IQueryHandler<GetStateById, StateDto> _getStateByIdHandler;
    private readonly IQueryHandler<GetStates, IEnumerable<StateDto>> _getStatesdHandler;

    public StateController(ICommandHandler<CreateState> createStateHandler,
        IQueryHandler<GetStateById, StateDto> getStateByIdHandler,
        IQueryHandler<GetStates, IEnumerable<StateDto>> getStatesdHandler)
    {
        _createStateHandler = createStateHandler;
        _getStateByIdHandler = getStateByIdHandler;
        _getStatesdHandler = getStatesdHandler;
    }
    
    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<StateDto>>> Get()
    {
        var states = await _getStatesdHandler.HandleAsync(new GetStates { });
        
        if (states is null)
        {
            return NotFound();
        }

        return Ok(states);
    }
    
    [HttpGet("{stateId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StateDto>> Get(Guid stateId)
    {
        var state = await _getStateByIdHandler.HandleAsync(new GetStateById { StateId = stateId});
        if (state is null)
        {
            return NotFound();
        }

        return state;
    }

    [HttpPost]
    [SwaggerOperation("Create state")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(CreateState command)
    {
        command = command with {Id = Guid.NewGuid()};
        await _createStateHandler.HandleAsync(command);
        var route = "Get";
        return CreatedAtAction(route, new {command.Id}, null);
    }
    
  
}