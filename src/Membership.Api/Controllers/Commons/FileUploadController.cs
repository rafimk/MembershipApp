using System;
using System.Threading.Tasks;
using Membership.Core.Consts;
using Membership.Infrastructure;
using Membership.Infrastructure.FileManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Membership.Api.Controllers.Commons;

[ApiController]
[Route("[controller]")]
public class FileUploadController : ControllerBase
{
    readonly IBufferedFileUploadService _bufferedFileUploadService;
    private readonly FileUploadOptions _fileUploadOptions;

    public FileUploadController(IBufferedFileUploadService bufferedFileUploadService, IOptions<FileUploadOptions> fileOptions)
    {
        _bufferedFileUploadService = bufferedFileUploadService;
        _fileUploadOptions = fileOptions.Value;
    }
    
    [HttpPost("photo")]
    public async Task<ActionResult> PhotoUpload(IFormFile file)
    {
        var fileId = await _bufferedFileUploadService.UploadFile(file, _fileUploadOptions.FilePath, FileType.Photo);

        return Ok(fileId);
    }
    
    [HttpPost("emirates-id-front")]
    public async Task<ActionResult> EmiratesIdFront(IFormFile file)
    {
        var fileId = await _bufferedFileUploadService.UploadFile(file, _fileUploadOptions.FilePath, FileType.EmiratesIdFront);

        return Ok(fileId);
    }
    
    [HttpPost("emirates-id-back")]
    public async Task<ActionResult> EmiratesIdBack(IFormFile file)
    {
        var fileId = await _bufferedFileUploadService.UploadFile(file, _fileUploadOptions.FilePath, FileType.EmiratesIdBack);

        return Ok(fileId);
    }
    
    [HttpPost("passport-first")]
    public async Task<ActionResult> PassportFirst(IFormFile file)
    {
        var fileId = await _bufferedFileUploadService.UploadFile(file, _fileUploadOptions.FilePath, FileType.PassportFirst);

        return Ok(fileId);
    }
    
    [HttpPost("passport-last")]
    public async Task<ActionResult> PassportLast(IFormFile file)
    {
        var fileId = await _bufferedFileUploadService.UploadFile(file, _fileUploadOptions.FilePath, FileType.PassportLast);

        return Ok(fileId);
    }

    [HttpGet("{fileId:guid}"), DisableRequestSizeLimit]
    public async Task<ActionResult> Download(Guid fileId)
    {
        var result = await _bufferedFileUploadService.Download(fileId, _fileUploadOptions.FilePath);
        return File(result.Memory, result.FileType, result.FileName);
    }
}