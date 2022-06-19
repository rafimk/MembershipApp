using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Memberships;
using Membership.Core.Repositories.Memberships;

namespace Membership.Application.Commands.Memberships.Members.Handlers;

internal sealed class DeactivateMemberHandler : ICommandHandler<DeactivateMember>
{
    private readonly IMemberRepository _repository;

    public DeactivateMemberHandler(IMemberRepository repository)
        => _repository = repository;

    public async Task HandleAsync(DeactivateMember command)
    {
        var member = await _repository.GetByIdAsync(command.MemberId);

        if (member is null)
        {
            throw new MemberNotFoundException(command.MemberId);
        }
        
        member.Deactivate();
    }
}