using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Memberships.Members;

public record UpdateMember : ICommand
{
    public Guid Id { get; set; }
    public DateTime EmiratesIdExpiry { get; set; }
    public Guid? EmiratesIdFrontPage { get; set;}
    public Guid? EmiratesIdLastPage { get; set;}
    public DateTime DateOfBirth { get; set; } 
    public string MobileNumber { get; set; }
    public string AlternativeContactNumber { get; set; }
    public string Email { get; set; }
    public string PassportNumber { get; set; }
    public DateTime PassportExpiry { get; set; }
    public Guid? PassportFrontPage { get; set;}
    public Guid? PassportLastPage { get; set;}
    public Guid ProfessionId { get; set; }
    public Guid QualificationId { get; set; }
    public int BloodGroup { get; set; }
    public int Gender { get; set; } = 0;
    public Guid? Photo { get; set;}
    public string HouseName { get; set; }
    public string AddressInIndia { get; set; }
    public Guid? AddressInDistrictId { get; set; }
    public Guid? AddressInMandalamId { get; set; }
    public Guid? AddressInPanchayatId { get; set; }
    public Guid? RegisteredOrganizationId { get; set; }
    public Guid? WelfareSchemeId { get; set; }
}