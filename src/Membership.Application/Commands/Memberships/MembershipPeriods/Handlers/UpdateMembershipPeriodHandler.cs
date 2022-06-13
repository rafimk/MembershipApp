using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Memberships;
using Membership.Core.Repositories.Memberships;

namespace Membership.Application.Commands.Memberships.MembershipPeriods.Handlers;

internal sealed class UpdateMembershipPeriodHandler : ICommandHandler<UpdateMembershipPeriod>
{
    private readonly IMembershipPeriodRepository _repository;

    public UpdateMembershipPeriodHandler(IMembershipPeriodRepository repository)
        => _repository = repository;

    public async Task HandleAsync(UpdateMembershipPeriod command)
    {
        var membershipPeriod = await _repository.GetByIdAsync(command.MembershipPeriodId);

        if (membershipPeriod is null)
        {
            throw new MembershipPeriodNotFoundException(command.MembershipPeriodId);
        }
        membershipPeriod.Update(command.Start, command.End, command.RegistrationUntil);
        await _repository.UpdateAsync(membershipPeriod);
    }
}