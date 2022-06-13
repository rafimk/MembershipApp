using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Memberships;
using Membership.Core.Repositories.Memberships;

namespace Membership.Application.Commands.Memberships.MembershipPeriods.Handlers;

internal sealed class ActivateMembershipPeriodHandler : ICommandHandler<ActivateMembershipPeriod>
{
    private readonly IMembershipPeriodRepository _repository;

    public ActivateMembershipPeriodHandler(IMembershipPeriodRepository repository)
        => _repository = repository;

    public async Task HandleAsync(ActivateMembershipPeriod command)
    {
        var membershipPeriod = await _repository.GetByIdAsync(command.MembershipPeriodId);

        if (membershipPeriod is null)
        {
            throw new MembershipPeriodNotFoundException(command.MembershipPeriodId);
        }
        membershipPeriod.Activate();
        membershipPeriod.Update(membershipPeriod.Start, membershipPeriod.End, membershipPeriod.RegistrationUntil);
        await _repository.UpdateAsync(membershipPeriod);
    }
}