using Membership.Application.DTO.Memberships;
using Membership.Application.DTO.Nationalities;
using Membership.Application.DTO.Users;

namespace Membership.Application.DTO.Commons;

public class MyLookupsDto
{
    public IEnumerable<string> ApplicableUserRole { get; set; }
    public IEnumerable<CascadeDto> CascadeData { get; set; }
    public IEnumerable<AreaDto> Areas { get; set; }
    public IEnumerable<PanchayatDto> Panchayats { get; set; }
    public IEnumerable<ProfessionDto> Professions { get; set; }
    public IEnumerable<QualificationDto> Qualifications { get; set; }
    public string CascadeTitle { get; set; }
}