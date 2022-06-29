using Membership.Application.DTO.Memberships;
using Membership.Application.DTO.Nationalities;
using Membership.Application.DTO.Users;

namespace Membership.Application.DTO.Commons;

public class BufferedFileUploadDto
{
    public MemoryStream Memory { get; set;}
    public string FileType { get; set; }
    public string FileName { get; set; }
}