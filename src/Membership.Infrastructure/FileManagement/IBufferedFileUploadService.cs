using Membership.Application.DTO.Commons;
using Membership.Application.DTO.Memberships;
using Membership.Core.Consts;
using Microsoft.AspNetCore.Http;

namespace Membership.Infrastructure.FileManagement;

public interface IBufferedFileUploadService
{
    Task<Guid?> UploadFile(IFormFile file, string filePath, FileType type, Guid? userId);

    Task<BufferedFileUploadDto> Download(Guid fileGuid, string filePath);
    
    BufferedFileUploadDto MembersExcelReportDownload(IEnumerable<MemberListDto> memberList);
}