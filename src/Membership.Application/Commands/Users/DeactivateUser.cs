using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Users;

public record DeactivateUser : ICommand
{
    public Guid UserId { get; set; }
}

