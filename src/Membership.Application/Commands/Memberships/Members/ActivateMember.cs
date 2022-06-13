using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Memberships.Members;

public record ActivateMember(Guid? MemberId): ICommand;