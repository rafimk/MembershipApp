using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Membership.Api.Controllers.Commons;
using Membership.Application.Abstractions;
using Membership.Application.Commands.Memberships.Disputes;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Disputes;
using Membership.Application.Queries.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Membership.Api.Controllers.Memberships;

public class DisputesController : ApiController
{
    private readonly ICommandHandler<CreateDisputeRequest> _createDisputeRequestHandler;
    private readonly ICommandHandler<UpdateDisputeRequest> _updateDisputeRequestHandler;
    private readonly ICommandHandler<ApproveDisputeRequest> _approveDisputeRequestHandler;
    private readonly ICommandHandler<RejectDisputeRequest> _rejectDisputeRequestHandler;
    private readonly IQueryHandler<GetDisputeRequestById, DisputeRequestDto> _getDisputeRequestByIdHandler;
    private readonly IQueryHandler<GetDisputeRequestByMandalamId, IEnumerable<DisputeRequestDto>> _getRequestByMandalamIdHandler;
    private readonly IQueryHandler<GetDisputeRequestByRole, PaginatedResult<DisputeRequestListDto>> _getDisputeRequestByRoleHandler;
    private readonly IQueryHandler<GetDisputeRequests, IEnumerable<DisputeRequestDto>> _getDisputeRequestsHandler;
    private readonly IQueryHandler<GetDisputeRequestsByAreaId, IEnumerable<DisputeRequestDto>> _getDisputeRequestsByAreaIdHandler;
    private readonly IQueryHandler<GetDisputeRequestsByStateId, IEnumerable<DisputeRequestDto>> _getDisputeRequestsByStateIdHandler;

    public DisputesController(ICommandHandler<CreateDisputeRequest> createDisputeRequestHandler, 
        ICommandHandler<UpdateDisputeRequest> updateDisputeRequestHandler,
        ICommandHandler<ApproveDisputeRequest> approveDisputeRequestHandler, 
        ICommandHandler<RejectDisputeRequest> rejectDisputeRequestHandler,
        IQueryHandler<GetDisputeRequestById, DisputeRequestDto> getDisputeRequestByIdHandler,
        IQueryHandler<GetDisputeRequestByMandalamId, IEnumerable<DisputeRequestDto>> getRequestByMandalamIdHandler,
        IQueryHandler<GetDisputeRequestByRole, PaginatedResult<DisputeRequestListDto>> getDisputeRequestByRoleHandler,
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
    
    // [HttpGet()]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // public async Task<ActionResult<IEnumerable<DisputeRequestDto>>> Get()
    // {
    //     var disputeRequests = await _getDisputeRequestsHandler.HandleAsync(new GetDisputeRequests { });
    //     
    //     if (disputeRequests is null)
    //     {
    //         return NotFound();
    //     }
    //
    //     return Ok(disputeRequests);
    // }
    
    [Authorize(Roles = "mandalam-agent, district-agent, dispute-committee")]
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
    
    // [HttpGet("mandalam/{mandalamId:guid}")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // public async Task<ActionResult<IEnumerable<DisputeRequestDto>>> GetByMandalamId(Guid mandalamId)
    // {
    //     var disputeRequests = await _getRequestByMandalamIdHandler.HandleAsync(new GetDisputeRequestByMandalamId { MandalamId = mandalamId});
    //     
    //     if (disputeRequests is null)
    //     {
    //         return NotFound();
    //     }
    //
    //     return Ok(disputeRequests);
    // }
    
    [Authorize(Roles = "mandalam-agent, district-agent")]
    [HttpGet("role")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PaginatedResult<DisputeRequestListDto>>> GetByRole()
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
    
    // [HttpGet("area/{areaId:guid}")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // public async Task<ActionResult<IEnumerable<DisputeRequestDto>>> GetByAreaId(Guid areaId)
    // {
    //     var disputeRequests = await _getDisputeRequestsByAreaIdHandler.HandleAsync(new GetDisputeRequestsByAreaId { AreaId = areaId });
    //     
    //     if (disputeRequests is null)
    //     {
    //         return NotFound();
    //     }
    //
    //     return Ok(disputeRequests);
    // }
    
    // [HttpGet("state/{stateId:guid}")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // public async Task<ActionResult<IEnumerable<DisputeRequestDto>>> GetByStateId(Guid stateId)
    // {
    //     var disputeRequests = await _getDisputeRequestsByStateIdHandler.HandleAsync(new GetDisputeRequestsByStateId { StateId = stateId });
    //     
    //     if (disputeRequests is null)
    //     {
    //         return NotFound();
    //     }
    //
    //     return Ok(disputeRequests);
    // }
    
    [Authorize(Roles = "mandalam-agent, district-agent")]
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
    
    // [HttpPut("{requestId:guid}")]
    // public async Task<ActionResult> Put(Guid requestId, UpdateDisputeRequest command)
    // {
    //     var userId = Guid.Parse(User?.Identity?.Name);
    //     command = command with {SubmittedBy = userId};
    //     await _updateDisputeRequestHandler.HandleAsync(command with {Id = requestId});
    //     return NoContent();
    // }
    
    [Authorize(Roles = "dispute-committee")]
    [HttpPut("approve/{requestId:guid}")]
    public async Task<ActionResult> Approve(Guid requestId, ApproveDisputeRequest command)
    {
        var userId = Guid.Parse(User?.Identity?.Name);
        command = command with {ActionBy = userId};
        await _approveDisputeRequestHandler.HandleAsync(command);
        return NoContent();
    }
    
    [Authorize(Roles = "dispute-committee")]
    [HttpPut("reject/{requestId:guid}")]
    public async Task<ActionResult> Reject(Guid requestId, RejectDisputeRequest command)
    {
        var userId = Guid.Parse(User?.Identity?.Name);
        command = command with {ActionBy = userId};
        await _rejectDisputeRequestHandler.HandleAsync(command);
        return NoContent();
    }
}