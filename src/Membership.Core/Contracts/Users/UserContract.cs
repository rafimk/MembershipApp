using Membership.Core.ValueObjects;

namespace Membership.Core.Contracts.Users;

public record UserContract()
{
    public Guid Id { get; set; }
    public FullName FullName { get; set; }
    public Email Email { get; set; }
    public MobileNumber MobileNumber { get; set; } 
    public UserRole Role { get; set; }
    public string Designation { get; set; }
    public string PasswordHash { get; set; }
    public Guid? StateId { get; set; }
    public Guid? DistrictId { get; set; }
    public Guid? CascadeId { get; set; }
    public string CascadeName { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
    public bool IsDisputeCommittee {get; set;}
}