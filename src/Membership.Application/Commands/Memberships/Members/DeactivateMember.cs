using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Memberships.Members;

public record DeactivateMember(Guid MemberId): ICommand;