using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Memberships;
using Membership.Core.Abstractions;
using Membership.Core.Repositories.Memberships;

namespace Membership.Application.Commands.Memberships.MembershipPeriods.Handlers;

internal sealed class EnrollDeactivateMembershipPeriodHandler : ICommandHandler<EnrollDeactivateMembershipPeriod>
{
    private readonly IMembershipPeriodRepository _repository;
    private readonly IClock _clock;

    public EnrollDeactivateMembershipPeriodHandler(IMembershipPeriodRepository repository, IClock clock)
    {
        _repository = repository;
        _clock = clock;
    }

    public async Task HandleAsync(EnrollDeactivateMembershipPeriod command)
    {
        var membershipPeriod = await _repository.GetByIdAsync((Guid)command.MembershipPeriodId);

        if (membershipPeriod is null)
        {
            throw new MembershipPeriodNotFoundException(command.MembershipPeriodId);
        }
        membershipPeriod.EnrollDeactivate(_clock.Current());
        await _repository.UpdateAsync(membershipPeriod);
    }
}