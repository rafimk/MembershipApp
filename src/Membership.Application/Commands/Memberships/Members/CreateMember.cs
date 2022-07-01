using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Memberships.Members;

public record CreateMember() : ICommand
{
    public Guid? Id { get; set; }
    public string FullName { get; set; } 
    public string EmiratesIdNumber { get; set; }
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
    public Guid? Photo { get; set;}
    public string HouseName { get; set; }
    public string AddressInIndia { get; set; }
    public string PasswordHash { get; set; }
    public Guid AreaId { get; set; }
    public Guid PanchayatId { get; set; } 
    public Guid? RegisteredOrganizationId { get; set; }
    public Guid? WelfareSchemeId { get; set; }
    public Guid? AgentId { get; set; }
}