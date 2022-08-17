using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Memberships;
using Membership.Application.Exceptions.Nationalities;
using Membership.Core.Entities.Memberships.Disputes;
using Membership.Core.Contracts.Memberships;
using Membership.Core.Repositories.Memberships;
using Membership.Core.Repositories.Nationalities;
using Membership.Core.Repositories.Users;
using Membership.Core.ValueObjects;
using Membership.Core.Abstractions;

namespace Membership.Application.Commands.Memberships.Disputes.Handlers;

internal sealed class CreateDisputeRequestHandler : ICommandHandler<CreateDisputeRequest>
{
    private readonly IDisputeRequestRepository _disputeRequestRepository;
    private readonly IMemberRepository _memberRepository;
    private readonly IAreaRepository _areaRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPanchayatRepository _panchayatRepository;
    private readonly IClock _clock;

    public CreateDisputeRequestHandler(IDisputeRequestRepository disputeRequestRepository, IMemberRepository memberRepository, 
        IAreaRepository areaRepository, IUserRepository userRepository, IMembershipPeriodRepository membershipPeriodRepository, 
        IPanchayatRepository panchayatRepository, IClock clock)
    {
        _disputeRequestRepository = disputeRequestRepository;
        _memberRepository = memberRepository;
        _areaRepository = areaRepository;
        _userRepository = userRepository;
        _panchayatRepository = panchayatRepository;
        _clock = clock;
    }

    public async Task HandleAsync(CreateDisputeRequest command)
    {
        var agent = await _userRepository.GetByIdAsync((Guid)command.SubmittedBy);

        if (agent is null)
        {
            throw new AgentNotFoundException(command.SubmittedBy);
        }

        var member = await _memberRepository.GetByIdAsync(command.MemberId);
        
        if (member is null)
        {
            throw new MemberNotFoundException(command.MemberId);
        }
        
        var area = await _areaRepository.GetByIdAsync(command.ProposedAreaId);
        
        if (area is null)
        {
            throw new AreaNotFoundException(command.ProposedAreaId);
        }
        
        var applicableAreas = await _areaRepository.GetByStateIdAsync((Guid)agent.StateId);

        var findArea = applicableAreas.FirstOrDefault(x => x.Id == command.ProposedAreaId);
 
        if (findArea is null)
        {
            throw new NotAuthorizedToCreateMemberForThisAreaException();
        }

        var panchayat = await _panchayatRepository.GetByIdAsync(command.ProposedPanchayatId);

        if (panchayat is null)
        {
            throw new InvalidPanchayatException();
        }

        var availableDisputeRequest = await _disputeRequestRepository.GetPendingByMemberIdAsync(command.MemberId);
        
        if (availableDisputeRequest is null)
        {
            throw new PendingDisputeAlreadyAvailableException();
        }

        var disputeReques = DisputeRequest.Create(new CreateDisputeRequestContract
        {
            Id = (Guid)command.Id,
            MemberId = command.MemberId,
            StateId = (Guid)agent.StateId,
            ProposedAreaId = command.ProposedAreaId,
            DistrictId = (Guid)agent.DistrictId,
            ProposedMandalamId = panchayat.MandalamId,
            ProposedPanchayatId = command.ProposedPanchayatId,
            Reason = command.Reason,
            SubmittedDate = _clock.Current(),
            SubmittedBy = (Guid)command.SubmittedBy
        });
        
        await _disputeRequestRepository.AddAsync(disputeReques);
    }
}