using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Users;

public record ChangeUserPassword : ICommand
{
    public Guid? UserId { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}