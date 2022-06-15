using Membership.Application.Abstractions;

namespace Membership.Application.Commands;

public record SignIn(string Email, string Password) : ICommand;