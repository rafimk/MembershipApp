using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Users;

public record ActivateUser(Guid UserId): ICommand;