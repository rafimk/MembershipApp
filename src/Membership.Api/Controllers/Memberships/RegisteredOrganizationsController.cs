using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Membership.Api.Controllers.Commons;
using Membership.Application.Abstractions;
using Membership.Application.Commands.Memberships.RegisteredOrganizations;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.RegisteredOrganizations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Membership.Api.Controllers.Memberships;

public class RegisteredOrganizationsController : ApiController
{
    private readonly ICommandHandler<CreateRegisteredOrganization> _createRegisteredOrganizationHandler;
    private readonly ICommandHandler<UpdateRegisteredOrganization> _updateRegisteredOrganizationHaneHandler;
    private readonly ICommandHandler<DeleteRegisteredOrganization> _deleteRegisteredOrganizationHaneHandler;
    private readonly IQueryHandler<GetRegisteredOrganizationById, RegisteredOrganizationDto> _getRegisteredOrganizationByIdHandler;
    private readonly IQueryHandler<GetRegisteredOrganizations, IEnumerable<RegisteredOrganizationDto>> _getRegisteredOrganizationsdHandler;

    public RegisteredOrganizationsController(ICommandHandler<CreateRegisteredOrganization> createRegisteredOrganizationHandler,
        ICommandHandler<UpdateRegisteredOrganization> updateRegisteredOrganizationHaneHandler,
        ICommandHandler<DeleteRegisteredOrganization> deleteRegisteredOrganizationHaneHandler,
        IQueryHandler<GetRegisteredOrganizationById, RegisteredOrganizationDto> getRegisteredOrganizationByIdHandler,
        IQueryHandler<GetRegisteredOrganizations, IEnumerable<RegisteredOrganizationDto>> getRegisteredOrganizationsdHandler)
    {
        _createRegisteredOrganizationHandler = createRegisteredOrganizationHandler;
        _updateRegisteredOrganizationHaneHandler = updateRegisteredOrganizationHaneHandler;
        _deleteRegisteredOrganizationHaneHandler = deleteRegisteredOrganizationHaneHandler;
        _getRegisteredOrganizationByIdHandler = getRegisteredOrganizationByIdHandler;
        _getRegisteredOrganizationsdHandler = getRegisteredOrganizationsdHandler;
    }
    
    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<RegisteredOrganizationDto>>> Get()
    {
        var RegisteredOrganizations = await _getRegisteredOrganizationsdHandler.HandleAsync(new GetRegisteredOrganizations { });
        
        if (RegisteredOrganizations is null)
        {
            return NotFound();
        }

        return Ok(RegisteredOrganizations);
    }
    
    [HttpGet("{RegisteredOrganizationId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RegisteredOrganizationDto>> Get(Guid RegisteredOrganizationId)
    {
        var RegisteredOrganization = await _getRegisteredOrganizationByIdHandler.HandleAsync(new GetRegisteredOrganizationById { RegisteredOrganizationId = RegisteredOrganizationId});
        if (RegisteredOrganization is null)
        {
            return NotFound();
        }

        return RegisteredOrganization;
    }

    // [HttpPost]
    // [SwaggerOperation("Create RegisteredOrganization")]
    // [ProducesResponseType(StatusCodes.Status201Created)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // public async Task<ActionResult> Post(CreateRegisteredOrganization command)
    // {
    //     command = command with { RegisteredOrganizationId = Guid.NewGuid()};
    //     await _createRegisteredOrganizationHandler.HandleAsync(command);
    //     return CreatedAtAction(nameof(Get), command, null);
    // }
    //
    // [HttpPut("{RegisteredOrganizationId:guid}")]
    // public async Task<ActionResult> Put(Guid RegisteredOrganizationId, UpdateRegisteredOrganization command)
    // {
    //     await _updateRegisteredOrganizationHaneHandler.HandleAsync(command with { RegisteredOrganizationId = RegisteredOrganizationId});
    //     return NoContent();
    // }
    //
    // [HttpDelete("{RegisteredOrganizationId:guid}")]
    // public async Task<ActionResult> Delete(Guid RegisteredOrganizationId)
    // {
    //     await _deleteRegisteredOrganizationHaneHandler.HandleAsync( new DeleteRegisteredOrganization { RegisteredOrganizationId = RegisteredOrganizationId});
    //     return NoContent();
    // }
}