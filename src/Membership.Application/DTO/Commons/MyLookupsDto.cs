using Membership.Application.DTO.Users;

namespace Membership.Application.DTO.Commons;

public class MyLookupsDto
{
    public IEnumerable<string> ApplicableUserRole { get; set; }
    public IEnumerable<CascadeDto> CascadeData { get; set; }
    public string CascadeTitle { get; set; }
}