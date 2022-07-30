using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Users;

public record ResetPassword : ICommand
{
    public string Email { get; set; }
    public string OldPassword { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}