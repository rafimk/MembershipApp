using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Membership.Api.Controllers.Commons;
using Membership.Application.Abstractions;
using Membership.Application.Commands.Nationalities.States;
using Membership.Application.DTO.Nationalities;
using Membership.Application.Queries.Nationalities.States;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Membership.Api.Controllers.Nationalities;

public class StatesController : ApiController
{
    private readonly ICommandHandler<CreateState> _createStateHandler;
    private readonly ICommandHandler<UpdateState> _updateStateHaneHandler;
    private readonly ICommandHandler<DeleteState> _deleteStateHaneHandler;
    private readonly IQueryHandler<GetStateById, StateDto> _getStateByIdHandler;
    private readonly IQueryHandler<GetStates, IEnumerable<StateDto>> _getStatesdHandler;

    public StatesController(ICommandHandler<CreateState> createStateHandler,
        ICommandHandler<UpdateState> updateStateHaneHandler,
        ICommandHandler<DeleteState> deleteStateHaneHandler,
        IQueryHandler<GetStateById, StateDto> getStateByIdHandler,
        IQueryHandler<GetStates, IEnumerable<StateDto>> getStatesdHandler)
    {
        _createStateHandler = createStateHandler;
        _updateStateHaneHandler = updateStateHaneHandler;
        _deleteStateHaneHandler = deleteStateHaneHandler;
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

    // [HttpPost]
    // [SwaggerOperation("Create state")]
    // [ProducesResponseType(StatusCodes.Status201Created)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // public async Task<ActionResult> Post(CreateState command)
    // {
    //     command = command with {Id = Guid.NewGuid()};
    //     await _createStateHandler.HandleAsync(command);
    //     return CreatedAtAction(nameof(Get), command.Id, null);
    // }
    //
    // [HttpPut("{stateId:guid}")]
    // public async Task<ActionResult> Put(Guid stateId, UpdateState command)
    // {
    //     await _updateStateHaneHandler.HandleAsync(command with {StateId = stateId});
    //     return NoContent();
    // }
    //
    // [HttpDelete("{stateId:guid}")]
    // public async Task<ActionResult> Delete(Guid stateId)
    // {
    //     await _deleteStateHaneHandler.HandleAsync( new DeleteState {StateId = stateId});
    //     return NoContent();
    // }
}