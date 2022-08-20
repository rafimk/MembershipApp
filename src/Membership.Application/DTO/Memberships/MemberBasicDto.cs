namespace Membership.Application.DTO.Memberships;

public class MemberBasicDto
{
    public Guid Id { get; set; }
    public string MembershipId { get; set; }
    public string FullName { get; set; }
    public string EmiratesIdNumber { get; set; }
    public DateTimeOffset EmiratesIdExpiry { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public string MobileNumber { get; set; }
    public string Email { get; set; }
    public int BloodGroup { get; set; }
    public int Gender { get; set; } = 0;
    public string HouseName { get; set; }
    public string AddressInIndia { get; set; }
}