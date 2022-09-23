using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Memberships;
using Membership.Core.Abstractions;
using Membership.Core.Entities.Memberships.Members;
using Membership.Core.Repositories.Memberships;
using Membership.Core.Repositories.Users;

namespace Membership.Application.Commands.Memberships.Members.Handlers;

internal sealed class ReserveVerificationHandler : ICommandHandler<ReserveVerification>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMembershipVerificationRepository _membershipVerificationRepository;
    private readonly IClock _clock;

    public ReserveVerificationHandler(IMemberRepository memberRepository, IUserRepository userRepository,
        IMembershipVerificationRepository membershipVerificationRepository,IClock clock)
    {
        _memberRepository = memberRepository;
        _membershipVerificationRepository = membershipVerificationRepository;
        _userRepository = userRepository;
        _clock = clock;
    }

    public async Task HandleAsync(ReserveVerification command)
    {
        var user = await _userRepository.GetByIdAsync((Guid)command.VerifiedUserId);

        if (user is null)
        {
            throw new AgentNotFoundException(command.VerifiedUserId);
        }

        var nextMember = await _memberRepository.GetNextMemberForVerification();

        if (nextMember is null)
        {
            throw new AllMembersVerifiedException();
        }

        var membershipVerification = MembershipVerification.Create((Guid)command.Id, nextMember.Id, 
            user.Id, (Guid)nextMember.EmiratesIdFrontPage, (Guid)nextMember.EmiratesIdLastPage,
            nextMember.PassportFrontPage, nextMember.PassportLastPage);
        
        await _membershipVerificationRepository.AddAsync(membershipVerification);
        
        nextMember.VerifyStarted((Guid)command.Id);

        await _memberRepository.UpdateAsync(nextMember);
    }
}