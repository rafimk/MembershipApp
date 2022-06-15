using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Users;

public record VerifyUser(Guid UserId, string VerifyCode): ICommand;