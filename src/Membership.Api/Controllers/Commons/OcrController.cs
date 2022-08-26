using System;
using System.Threading.Tasks;
using Membership.Application.Abstractions;
using Membership.Application.DTO.Commons;
using Membership.Application.Queries.Commons;
using Membership.Core.Entities.Commons;
using Membership.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace Membership.Api.Controllers.Commons;

public class OcrController : ApiController
{
    private readonly IQueryHandler<OcrDataRead, OcrDataDto> _ocrDataReadHandler;
    private readonly FileUploadOptions _fileUploadOptions;
    
    public OcrController(IQueryHandler<OcrDataRead, OcrDataDto> ocrDataReadHandler, IOptions<FileUploadOptions> fileOptions)
    {
        _ocrDataReadHandler = ocrDataReadHandler;
        _fileUploadOptions = fileOptions.Value;
    }
    
    [Authorize(Roles = "mandalam-agent, district-agent")]
    [HttpPost]
    [SwaggerOperation("Read OCR Data")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(OcrDataRead query)
    {
        if (string.IsNullOrWhiteSpace(User.Identity?.Name))
        {
            return NotFound();
        }

        var userId = Guid.Parse(User?.Identity?.Name);
        
        query.UserId = userId;
        query.FilePath = _fileUploadOptions.FilePath;
        var result = await _ocrDataReadHandler.HandleAsync(query);

        return Ok(result);
    }
}