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
    public IEnumerable<RegisteredOrganizationDto> RegisteredOrganizations { get; set; }
    public IEnumerable<WelfareSchemeDto> WelfareSchemes { get; set; }
    public MembershipPeriodDto MembershipPeriod { get; set; }
    public string CascadeTitle { get; set; }
    public string StateName { get; set; }
    public string DistrictsName { get; set; }
    public bool CanDisputeCommittee { get; set; } 
    
    public Guid? AgentDistrictId { get; set; }
    public Guid? AgentMandalamId { get; set; }
    public IEnumerable<DistrictDto> District { get; set; }
}