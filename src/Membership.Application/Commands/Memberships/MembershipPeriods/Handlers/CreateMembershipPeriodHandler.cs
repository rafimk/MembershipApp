using Membership.Application.Abstractions;
using Membership.Core.Entities.Memberships.MembershipPeriods;
using Membership.Core.Repositories.Memberships;
using Membership.Core.ValueObjects;

namespace Membership.Application.Commands.Memberships.MembershipPeriods.Handlers;

internal sealed class CreateMembershipPeriodHandler : ICommandHandler<CreateMembershipPeriod>
{
    private readonly IMembershipPeriodRepository _repository;

    public CreateMembershipPeriodHandler(IMembershipPeriodRepository repository)
        => _repository = repository;

    public async Task HandleAsync(CreateMembershipPeriod command)
    {
        var membershipPeriod = MembershipPeriod.Create(Guid.NewGuid(), (Date)command.Start , (Date)command.End, command.RegistrationUntil, DateTime.UtcNow);
        await _repository.AddAsync(membershipPeriod);
    }
}