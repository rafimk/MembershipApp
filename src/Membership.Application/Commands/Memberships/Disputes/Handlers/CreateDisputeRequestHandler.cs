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
    private readonly IClock _clock;

    public CreateDisputeRequestHandler(IDisputeRequestRepository disputeRequestRepository, IMemberRepository memberRepository, 
        IAreaRepository areaRepository, IUserRepository userRepository, IMembershipPeriodRepository membershipPeriodRepository, IClock clock)
    {
        _disputeRequestRepository = disputeRequestRepository;
        _memberRepository = memberRepository;
        _areaRepository = areaRepository;
        _userRepository = userRepository;
        _clock = clock;
    }

    public async Task HandleAsync(CreateDisputeRequest command)
    {
        var agent = await _userRepository.GetByIdAsync(command.SubmittedBy);

        if (agent is null)
        {
            throw new AgentNotFoundException(command.SubmittedBy);
        }
        
        if (await _memberRepository.GetByIdAsync(command.MemberId) is not null)
        {
            throw new MemberNotFoundException(command.MemberId);
        }
        
        var area = await _areaRepository.GetByIdAsync(command.ProposedAreaId);
        
        if (area is null)
        {
            throw new AreaNotFoundException(command.ProposedAreaId);
        }
        
        var applicableAreas = await _areaRepository.GetByStateIdAsync(agent.StateId);

        var findArea = applicableAreas.FirstOrDefault(x => x.Id == new GenericId(command.ProposedAreaId));
 
        if (findArea is null)
        {
            throw new NotAuthorizedToCreateMemberForThisAreaException();
        }
        
        var disputeReques = DisputeRequest.Create(new CreateDisputeRequestContract
        {
            Id = command.Id,
            MemberId = command.MemberId,
            ProposedAreaId = command.ProposedAreaId,
            ProposedMandalamId = command.ProposedMandalamId,
            ProposedPanchayatId = command.ProposedPanchayatId,
            Reason = command.Reason,
            SubmittedDate = _clock.Current(),
            SubmittedBy = command.SubmittedBy
        });
        
        await _disputeRequestRepository.AddAsync(disputeReques);
    }
}