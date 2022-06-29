using Membership.Core.ValueObjects;

namespace Membership.Core.Contracts.Users;

public record UpdateCreateContract()
{
    public FullName FullName { get; set; }
    public Email Email { get; set; }
    public MobileNumber MobileNumber { get; set; } 
    public MobileNumber AlternativeContactNumber { get; set; }
    public string Designation { get; set; }
    public bool IsDisputeCommittee {get; set;}
}