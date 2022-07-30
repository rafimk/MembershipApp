using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Users;

public record ForgetPassword : ICommand
{
    public string Email { get; set; }
}