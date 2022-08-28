using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Users;

public record ResetPassword : ICommand
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public Guid? LoggedUserId { get; set; }
}