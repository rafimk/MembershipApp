using Membership.Core.Consts;
using Membership.Core.ValueObjects;

namespace Membership.Core.Contracts.Memberships;

public record CreateMemberContract()
{
    public GenericId Id { get; set; }
    public MembershipId MembershipId { get; set; }
    public FullName FullName { get; set; } 
    public EmiratesIdNumber EmiratesIdNumber { get; set; }
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
    public Guid? Photo { get; set;}
    public string HouseName { get; set; }
    public string AddressInIndia { get; set; }
    public string PasswordHash { get; set; }
    public GenericId AreaId { get; set; }
    public GenericId MandalamId { get; set; } 
    public GenericId PanchayatId { get; set;}
    public Guid? RegisteredOrganizationId { get; set; }
    public Guid? WelfareSchemeId { get; set; }
    public GenericId MembershipPeriodId { get; set; }
    public string CardNumber { get; set; }
    public MemberStatus Status {get; set;}
    public DateTime CreatedAt { get; set; } 
    public GenericId CreatedBy { get; set; }
}