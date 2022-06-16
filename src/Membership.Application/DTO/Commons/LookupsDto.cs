using Membership.Application.DTO.Memberships;
using Membership.Application.DTO.Nationalities;

namespace Membership.Application.DTO.Commons;

public class LookupsDto
{
    public IEnumerable<AreaDto> Areas { get; set; }
    public IEnumerable<DistrictDto> Districts { get; set; }
    public IEnumerable<StateDto> States { get; set; }
    public IEnumerable<ProfessionDto> Professions { get; set; }
    public IEnumerable<QualificationDto> Qualifications { get; set; }
    public MembershipPeriodDto MembershipPeriod { get; set; }
}