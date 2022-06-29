using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Memberships;
using Membership.Core.Repositories.Memberships;

namespace Membership.Application.Commands.Memberships.MembershipPeriods.Handlers;

internal sealed class EnrollActivateMembershipPeriodHandler : ICommandHandler<EnrollActivateMembershipPeriod>
{
    private readonly IMembershipPeriodRepository _repository;
    private readonly IClock _clock;

    public EnrollActivateMembershipPeriodHandler(IMembershipPeriodRepository repository, IClock clock)
    {
        _repository = repository;
        _clock = clock;
    }

    public async Task HandleAsync(EnrollActivateMembershipPeriod command)
    {
        var membershipPeriod = await _repository.GetByIdAsync(command.MembershipPeriodId);

        if (membershipPeriod is null)
        {
            throw new MembershipPeriodNotFoundException(command.MembershipPeriodId);
        }
        membershipPeriod.EnrollActivate(_clock.Current());
        await _repository.UpdateAsync(membershipPeriod);
    }
}