using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Memberships;
using Membership.Core.Abstractions;
using Membership.Core.Repositories.Memberships;

namespace Membership.Application.Commands.Memberships.Members.Handlers;

internal sealed class UpdateVerificationHandler : ICommandHandler<UpdateVerification>
{
    private readonly IMembershipVerificationRepository _membershipVerificationRepository;
    private readonly IMemberRepository _memberRepository;
    private readonly IClock _clock;

    public UpdateVerificationHandler(IMembershipVerificationRepository membershipVerificationRepository, 
        IMemberRepository memberRepository, IClock clock)
    {
        _membershipVerificationRepository = membershipVerificationRepository;
        _memberRepository = memberRepository;
        _clock = clock;
    }

    public async Task HandleAsync(UpdateVerification command)
    {
        var membershipVerification = await _membershipVerificationRepository.GetByIdAsync(command.Id);

        if (membershipVerification is null)
        {
            throw new VerificationReservationNotFoundException(command.Id);
        }
        
        var member = await _memberRepository.GetByIdAsync(membershipVerification.MemberId);
        
        if (member is null)
        {
            throw new MemberNotFoundException(membershipVerification.MemberId);
        }

        membershipVerification.Verified(command.EdiFrontAndBackSideValid, command.EidNumberValid,
            command.EidFullNameValid, command.EidNationalityValid, command.EidDOBValid, command.EidDOEValid,
            command.EidIssuePlaceValid, command.Gender, command.CardType, 
            command.PassportFirstPageValid, command.PassportLastPageValid, _clock.Current());

        await _membershipVerificationRepository.UpdateAsync(membershipVerification);

        member.Verify(_clock.Current(), membershipVerification.Id);

        await _memberRepository.UpdateAsync(member);
    }
}