using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Memberships;
using Membership.Core.Repositories.Memberships;

namespace Membership.Application.Commands.Memberships.Members.Handlers;

internal sealed class ActivateMemberHandler : ICommandHandler<ActivateMember>
{
    private readonly IMemberRepository _repository;

    public ActivateMemberHandler(IMemberRepository repository)
        => _repository = repository;

    public async Task HandleAsync(ActivateMember command)
    {
        var member = await _repository.GetByIdAsync(command.MemberId);

        if (member is null)
        {
            throw new MemberNotFoundException(command.MemberId);
        }
        
        member.Activate();
    }
}