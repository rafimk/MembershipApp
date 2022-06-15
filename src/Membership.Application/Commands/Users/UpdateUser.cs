using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Users;

public record UpdateUser() : ICommand
{
    public Guid? UserId { get; set; }
    public string FullName { get; set; }
    public string MobileNumber { get; set; }
    public string AlternativeContactNumber { get; set; }
    public string Designation { get; set; }
}