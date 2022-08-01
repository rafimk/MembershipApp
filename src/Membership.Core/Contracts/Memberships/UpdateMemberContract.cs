using Membership.Core.Consts;
using Membership.Core.ValueObjects;

namespace Membership.Core.Contracts.Memberships;

public record UpdateMemberContract()
{
    public DateTime EmiratesIdExpiry { get; set; }
    public Guid? EmiratesIdFrontPage { get; set;}
    public Guid? EmiratesIdLastPage { get; set;}
    public DateTime DateOfBirth { get; set; } 
    public MobileNumber MobileNumber { get; set; }
    public OptionalMobileNumber AlternativeContactNumber { get; set; }
    public Email Email { get; set; }
    public PassportNumber PassportNumber { get; set; }
    public DateTime PassportExpiry { get; set; }
    public Guid? PassportFrontPage { get; set;}
    public Guid? PassportLastPage { get; set;}
    public GenericId ProfessionId { get; set; }
    public GenericId QualificationId { get; set; }
    public BloodGroup BloodGroup { get; set; }
    public Gender Gender { get; set; }
    public Guid? Photo { get; set;}
    public string HouseName { get; set; }
    public string AddressInIndia { get; set; }
    public Guid? AddressInDistrictId { get; set; }
    public Guid? AddressInMandalamId { get; set; }
    public Guid? AddressInPanchayatId { get; set; }
    public GenericId? RegisteredOrganizationId { get; set; }
    public GenericId? WelfareSchemeId { get; set; }
    public MemberStatus Status {get; set;}
}