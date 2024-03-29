using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Users;

public record CreateUser() : ICommand
{
    public Guid? Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string MobileNumber { get; set; }
    public string Designation { get; set; }
    public string Role { get; set; }
    public Guid? CascadeId { get; set; }
    public string CascadeName { get; set; }
    public Guid? LoggedUserId { get; set; }
    public bool IsDisputeCommittee { get; set; } = false;
}