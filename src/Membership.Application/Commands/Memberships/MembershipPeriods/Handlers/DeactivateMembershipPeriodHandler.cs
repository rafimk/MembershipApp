using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Memberships;
using Membership.Core.Repositories.Memberships;

namespace Membership.Application.Commands.Memberships.MembershipPeriods.Handlers;

internal sealed class DeactivateMembershipPeriodHandler : ICommandHandler<DeactivateMembershipPeriod>
{
    private readonly IMembershipPeriodRepository _repository;

    public DeactivateMembershipPeriodHandler(IMembershipPeriodRepository repository)
        => _repository = repository;

    public async Task HandleAsync(DeactivateMembershipPeriod command)
    {
        var membershipPeriod = await _repository.GetByIdAsync((Guid)command.MembershipPeriodId);

        if (membershipPeriod is null)
        {
            throw new MembershipPeriodNotFoundException(command.MembershipPeriodId);
        }
        membershipPeriod.Deactivate();
        await _repository.UpdateAsync(membershipPeriod);
    }
}