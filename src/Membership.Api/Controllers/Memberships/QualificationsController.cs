using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Membership.Application.Abstractions;
using Membership.Application.Commands.Memberships.Qualifications;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Qualifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Membership.Api.Controllers.Memberships;

[ApiController]
[Route("[controller]")]
public class QualificationsController : ControllerBase
{
    private readonly ICommandHandler<CreateQualification> _createQualificationHandler;
    private readonly ICommandHandler<UpdateQualification> _updateQualificationHaneHandler;
    private readonly ICommandHandler<DeleteQualification> _deleteQualificationHaneHandler;
    private readonly IQueryHandler<GetQualificationById, QualificationDto> _getQualificationByIdHandler;
    private readonly IQueryHandler<GetQualifications, IEnumerable<QualificationDto>> _getQualificationsdHandler;

    public QualificationsController(ICommandHandler<CreateQualification> createQualificationHandler,
        ICommandHandler<UpdateQualification> updateQualificationHaneHandler,
        ICommandHandler<DeleteQualification> deleteQualificationHaneHandler,
        IQueryHandler<GetQualificationById, QualificationDto> getQualificationByIdHandler,
        IQueryHandler<GetQualifications, IEnumerable<QualificationDto>> getQualificationsdHandler)
    {
        _createQualificationHandler = createQualificationHandler;
        _updateQualificationHaneHandler = updateQualificationHaneHandler;
        _deleteQualificationHaneHandler = deleteQualificationHaneHandler;
        _getQualificationByIdHandler = getQualificationByIdHandler;
        _getQualificationsdHandler = getQualificationsdHandler;
    }
    
    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<QualificationDto>>> Get()
    {
        var qualifications = await _getQualificationsdHandler.HandleAsync(new GetQualifications { });
        
        if (qualifications is null)
        {
            return NotFound();
        }

        return Ok(qualifications);
    }
    
    [HttpGet("{qualificationId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<QualificationDto>> Get(Guid qualificationId)
    {
        var qualification = await _getQualificationByIdHandler.HandleAsync(new GetQualificationById { QualificationId = qualificationId});
        if (qualification is null)
        {
            return NotFound();
        }

        return qualification;
    }

    [HttpPost]
    [SwaggerOperation("Create Qualification")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(CreateQualification command)
    {
        command = command with {QualificationId = Guid.NewGuid()};
        await _createQualificationHandler.HandleAsync(command);
        return CreatedAtAction(nameof(Get), command, null);
    }
    
    [HttpPut("{qualificationId:guid}")]
    public async Task<ActionResult> Put(Guid qualificationId, UpdateQualification command)
    {
        await _updateQualificationHaneHandler.HandleAsync(command with {QualificationId = qualificationId});
        return NoContent();
    }
    
    [HttpDelete("{qualificationId:guid}")]
    public async Task<ActionResult> Delete(Guid qualificationId)
    {
        await _deleteQualificationHaneHandler.HandleAsync( new DeleteQualification {QualificationId = qualificationId});
        return NoContent();
    }
}