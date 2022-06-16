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
public class QualificationsController : ControllerBase
{
    private readonly ICommandHandler<CreateQualification> _createQualificationHandler;
    private readonly ICommandHandler<UpdateQualification> _updateQualificationHaneHandler;
    private readonly ICommandHandler<DeleteQualification> _deleteQualificationHaneHandler;
    private readonly IQueryHandler<GetQualificationById, QualificationtDto> _getQualificationByIdHandler;
    private readonly IQueryHandler<GetQualifications, IEnumerable<QualificationtDto>> _getQualificationsdHandler;

    public QualificationsController(ICommandHandler<CreateQualification> createQualificationHandler,
        ICommandHandler<UpdateQualification> updateQualificationHaneHandler,
        ICommandHandler<DeleteQualification> deleteQualificationHaneHandler,
        IQueryHandler<GetQualificationById, QualificationtDto> getQualificationByIdHandler,
        IQueryHandler<GetQualifications, IEnumerable<DistrictDto>> getQualificationsdHandler)
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
    public async Task<ActionResult<IEnumerable<DistrictDto>>> Get()
    {
        var Qualifications = await _getQualificationsdHandler.HandleAsync(new GetDistricts { });
        
        if (Qualifications is null)
        {
            return NotFound();
        }

        return Ok(Qualifications);
    }
    
    [HttpGet("{QualificationId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DistrictDto>> Get(Guid QualificationId)
    {
        var Qualification = await _getQualificationByIdHandler.HandleAsync(new GetQualificationById { QualificationId = QualificationId});
        if (Qualification is null)
        {
            return NotFound();
        }

        return Qualification;
    }

    [HttpPost]
    [SwaggerOperation("Create Qualification")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(CreateQualification command)
    {
        command = command with {Id = Guid.NewGuid()};
        await _createQualificationHandler.HandleAsync(command);
        var route = "Get";
        return CreatedAtAction(route, new {command.Id}, null);
    }
    
    [HttpPut("{QualificationId:guid}")]
    public async Task<ActionResult> Put(Guid QualificationId, UpdateQualification command)
    {
        await _updateQualificationHaneHandler.HandleAsync(command with {QualificationId = QualificationId});
        return NoContent();
    }
    
    [HttpDelete("{QualificationId:guid}")]
    public async Task<ActionResult> Delete(Guid QualificationId)
    {
        await _deleteQualificationHaneHandler.HandleAsync( new DeleteQualification {QualificationId = QualificationId});
        return NoContent();
    }
}