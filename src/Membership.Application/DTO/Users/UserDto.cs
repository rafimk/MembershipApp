namespace Membership.Application.DTO.Users;

public class UserDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string MobileNumber { get; set; }
    public string AlternativeContactNumber { get; set; }
    public string Designation { get; set; }
    public string Role { get; set; }
    public Guid? CascadeId { get; set; }
    public string CascadeName { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive {get; set;}
    public DateTime? VerifiedAt { get; set; }
}