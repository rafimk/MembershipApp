using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Membership.Application.Abstractions;
using Membership.Application.Commands.Memberships.Disputes;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Disputes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Membership.Api.Controllers.Memberships;


[ApiController]
[Route("[controller]")]
public class DisputesController : ControllerBase
{
    private readonly ICommandHandler<CreateDisputeRequest> _createDisputeRequestHandler;
    private readonly ICommandHandler<UpdateDisputeRequest> _updateDisputeRequestHandler;
    private readonly ICommandHandler<ApproveDisputeRequest> _approveDisputeRequestHandler;
    private readonly ICommandHandler<RejectDisputeRequest> _rejectDisputeRequestHandler;
    private readonly IQueryHandler<GetDisputeRequestById, DisputeRequestDto> _getDisputeRequestByIdHandler;
    private readonly IQueryHandler<GetDisputeRequestByMandalamId, IEnumerable<DisputeRequestDto>> _getRequestByMandalamIdHandler;
    private readonly IQueryHandler<GetDisputeRequestByRole, IEnumerable<DisputeRequestDto>> _getDisputeRequestByRoleHandler;
    private readonly IQueryHandler<GetDisputeRequests, IEnumerable<DisputeRequestDto>> _getDisputeRequestsHandler;
    private readonly IQueryHandler<GetDisputeRequestsByAreaId, IEnumerable<DisputeRequestDto>> _getDisputeRequestsByAreaIdHandler;
    private readonly IQueryHandler<GetDisputeRequestsByStateId, IEnumerable<DisputeRequestDto>> _getDisputeRequestsByStateIdHandler;

    public DisputesController(ICommandHandler<CreateDisputeRequest> createDisputeRequestHandler, 
        ICommandHandler<UpdateDisputeRequest> updateDisputeRequestHandler,
        ICommandHandler<ApproveDisputeRequest> approveDisputeRequestHandler, 
        ICommandHandler<RejectDisputeRequest> rejectDisputeRequestHandler,
        IQueryHandler<GetDisputeRequestById, DisputeRequestDto> getDisputeRequestByIdHandler,
        IQueryHandler<GetDisputeRequestByMandalamId, IEnumerable<DisputeRequestDto>> getRequestByMandalamIdHandler,
        IQueryHandler<GetDisputeRequestByRole, IEnumerable<DisputeRequestDto>> getDisputeRequestByRoleHandler,
        IQueryHandler<GetDisputeRequests, IEnumerable<DisputeRequestDto>> getDisputeRequestsHandler,
        IQueryHandler<GetDisputeRequestsByAreaId, IEnumerable<DisputeRequestDto>> getDisputeRequestsByAreaIdHandler,
        IQueryHandler<GetDisputeRequestsByStateId, IEnumerable<DisputeRequestDto>> getDisputeRequestsByStateIdHandler)
    {
        _createDisputeRequestHandler = createDisputeRequestHandler;
        _updateDisputeRequestHandler = updateDisputeRequestHandler;
        _approveDisputeRequestHandler = approveDisputeRequestHandler;
        _rejectDisputeRequestHandler = rejectDisputeRequestHandler;
        _getDisputeRequestByIdHandler = getDisputeRequestByIdHandler;
        _getRequestByMandalamIdHandler = getRequestByMandalamIdHandler;
        _getDisputeRequestByRoleHandler = getDisputeRequestByRoleHandler;
        _getDisputeRequestsHandler = getDisputeRequestsHandler;
        _getDisputeRequestsByAreaIdHandler = getDisputeRequestsByAreaIdHandler;
        _getDisputeRequestsByStateIdHandler = getDisputeRequestsByStateIdHandler;
    }
    
    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<DisputeRequestDto>>> Get()
    {
        var disputeRequests = await _getDisputeRequestsHandler.HandleAsync(new GetDisputeRequests { });
        
        if (disputeRequests is null)
        {
            return NotFound();
        }

        return Ok(disputeRequests);
    }
    
    [HttpGet("{requestId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DisputeRequestDto>> Get(Guid requestId)
    {
        var disputeRequest = await _getDisputeRequestByIdHandler.HandleAsync(new GetDisputeRequestById { RequestId = requestId});
        
        if (disputeRequest is null)
        {
            return NotFound();
        }

        return disputeRequest;
    }
    
    [HttpGet("mandalam/{mandalamId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<DisputeRequestDto>>> GetByMandalamId(Guid mandalamId)
    {
        var disputeRequests = await _getRequestByMandalamIdHandler.HandleAsync(new GetDisputeRequestByMandalamId { MandalamId = mandalamId});
        
        if (disputeRequests is null)
        {
            return NotFound();
        }

        return Ok(disputeRequests);
    }
    
    [HttpGet("role")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<DisputeRequestDto>>> GetByRole()
    {
        if (string.IsNullOrWhiteSpace(User.Identity?.Name))
        {
            return NotFound();
        }

        var userId = Guid.Parse(User?.Identity?.Name);
        
        var disputeRequests = await _getDisputeRequestByRoleHandler.HandleAsync(new GetDisputeRequestByRole { UserId = userId });
        
        if (disputeRequests is null)
        {
            return NotFound();
        }

        return Ok(disputeRequests);
    }
    
    [HttpGet("area/{areaId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<DisputeRequestDto>>> GetByAreaId(Guid areaId)
    {
        var disputeRequests = await _getDisputeRequestsByAreaIdHandler.HandleAsync(new GetDisputeRequestsByAreaId { AreaId = areaId });
        
        if (disputeRequests is null)
        {
            return NotFound();
        }

        return Ok(disputeRequests);
    }
    
    [HttpGet("state/{stateId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<DisputeRequestDto>>> GetByStateId(Guid stateId)
    {
        var disputeRequests = await _getDisputeRequestsByStateIdHandler.HandleAsync(new GetDisputeRequestsByStateId { StateId = stateId });
        
        if (disputeRequests is null)
        {
            return NotFound();
        }

        return Ok(disputeRequests);
    }
    
    [HttpPost]
    [SwaggerOperation("Create Dispute Request")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(CreateDisputeRequest command)
    {
        if (string.IsNullOrWhiteSpace(User.Identity?.Name))
        {
            return NotFound();
        }

        var userId = Guid.Parse(User?.Identity?.Name);
        command = command with {Id =  Guid.NewGuid(), SubmittedBy = userId};
        await _createDisputeRequestHandler.HandleAsync(command);
        return CreatedAtAction(nameof(Get), new {command.Id}, null);
    }
    
    [HttpPut("{requestId:guid}")]
    public async Task<ActionResult> Put(Guid requestId, UpdateDisputeRequest command)
    {
        await _updateDisputeRequestHandler.HandleAsync(command with {Id = requestId});
        return NoContent();
    }
    
    [HttpPut("approve/{requestId:guid}")]
    public async Task<ActionResult> Approve(Guid requestId)
    {
        await _approveDisputeRequestHandler.HandleAsync( new ApproveDisputeRequest { RequestId = requestId});
        return NoContent();
    }
    
    [HttpPut("reject/{requestId:guid}")]
    public async Task<ActionResult> Reject(Guid requestId)
    {
        await _rejectDisputeRequestHandler.HandleAsync( new RejectDisputeRequest { RequestId = requestId});
        return NoContent();
    }
}