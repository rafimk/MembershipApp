using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Users;

public record DeactivateUser(Guid Id): ICommand;

