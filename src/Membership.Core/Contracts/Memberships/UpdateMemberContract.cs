using Membership.Core.Consts;
using Membership.Core.ValueObjects;

namespace Membership.Core.Contracts.Memberships;

public record UpdateMemberContract()
{
    public Date EmiratesIdExpiry { get; set; }
    public Guid? EmiratesIdFrontPage { get; set;}
    public Guid? EmiratesIdLastPage { get; set;}
    public Date DateOfBirth { get; set; } 
    public MobileNumber MobileNumber { get; set; }
    public MobileNumber AlternativeContactNumber { get; set; }
    public Email Email { get; set; }
    public PassportNumber PassportNumber { get; set; }
    public Date PassportExpiry { get; set; }
    public Guid? PassportFrontPage { get; set;}
    public Guid? PassportLastPage { get; set;}
    public GenericId ProfessionId { get; set; }
    public GenericId QualificationId { get; set; }
    public BloodGroup BloodGroup { get; set; }
    public Guid? Photo { get; set;}
    public string HouseName { get; set; }
    public string AddressInIndia { get; set; }
    public bool IsMemberOfAnyIndianRegisteredOrganization { get; set; }
    public bool IsKMCCWelfareScheme { get; set; }
    public DateTime CreatedAt { get; set; } 
    public GenericId CreatedBy { get; set; }
}