using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Users;

public record SignIn() : ICommand
{
    public string Email { get; set; }
    public string Password { get; set; }
}