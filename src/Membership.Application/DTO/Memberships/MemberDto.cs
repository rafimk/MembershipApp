using Membership.Application.DTO.Nationalities;
using Membership.Core.ValueObjects;

namespace Membership.Application.DTO.Memberships;

public class MemberDto
{
    public Guid Id { get; set; }
    public string MembershipId { get; set; }
    public string FullName { get; set; }
    public string EmiratesIdNumber { get; set; }
    public DateTimeOffset EmiratesIdExpiry { get; set; }
    public Guid? EmiratesIdFrontPage { get; set; }
    public Guid? EmiratesIdLastPage { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public string MobileNumber { get; set; }
    public string Email { get; set; }
    public string PassportNumber { get; set; }
    public DateTime? PassportExpiry { get; set; }
    public Guid? PassportFrontPage { get; set; }
    public Guid? PassportLastPage { get; set; }
    public ProfessionDto Profession { get; set; }
    public QualificationDto Qualification { get; set; }
    public int BloodGroup { get; set; }
    public int Gender { get; set; } = 0;
    public Guid? Photo { get; set; }
    public string HouseName { get; set; }
    public string AddressInIndia { get; set; }
    public DistrictDto AddressInDistrict { get; set; }
    public MandalamDto AddressInMandalam { get; set; }
    public PanchayatDto AddressInPanchayat { get; set; }
    public AreaDto Area { get; set; }
    public MandalamDto Mandalam { get; set; }
    public PanchayatDto Panchayat { get; set; }
    public Guid? RegisteredOrganizationId { get; set; }
    public Guid? WelfareSchemeId  { get; set; }
    public MembershipPeriodDto MembershipPeriod { get; set;}
    public double CollectedAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public bool IsActive { get; set; }
    public DateTime? VerifiedAt { get; set; }
}