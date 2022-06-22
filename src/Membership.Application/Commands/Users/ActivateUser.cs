using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Users;

public record ActivateUser : ICommand
{
    public Guid UserId { get; set; }
}