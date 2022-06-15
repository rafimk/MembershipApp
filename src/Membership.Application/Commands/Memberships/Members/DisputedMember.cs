using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Memberships.Members;

public record DisputedMember(Guid MemberId, Guid StateId): ICommand;