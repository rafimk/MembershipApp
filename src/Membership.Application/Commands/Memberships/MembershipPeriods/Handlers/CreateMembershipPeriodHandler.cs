using Membership.Application.Abstractions;
using Membership.Core.Entities.Memberships.MembershipPeriods;
using Membership.Core.Repositories.Memberships;
using Membership.Core.ValueObjects;

namespace Membership.Application.Commands.Memberships.MembershipPeriods.Handlers;

internal sealed class CreateMembershipPeriodHandler : ICommandHandler<CreateMembershipPeriod>
{
    private readonly IMembershipPeriodRepository _repository;
    private readonly IClock _clock;

    public CreateMembershipPeriodHandler(IMembershipPeriodRepository repository, IClock clock)
    {
        _repository = repository;
        _clock = clock;
    }

    public async Task HandleAsync(CreateMembershipPeriod command)
    {
        var membershipPeriod = MembershipPeriod.Create(command.MembershipPeriodId, (Date)command.Start , (Date)command.End, _clock.Current());
        await _repository.AddAsync(membershipPeriod);
    }
}