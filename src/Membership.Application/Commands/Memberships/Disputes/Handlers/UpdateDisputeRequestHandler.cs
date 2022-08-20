using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Memberships;
using Membership.Application.Exceptions.Nationalities;
using Membership.Core.Contracts.Memberships;
using Membership.Core.Repositories.Memberships;
using Membership.Core.Repositories.Nationalities;
using Membership.Core.Repositories.Users;

namespace Membership.Application.Commands.Memberships.Disputes.Handlers;

internal sealed class UpdateDisputeRequestHandler : ICommandHandler<UpdateDisputeRequest>
{
    private readonly IDisputeRequestRepository _disputeRequestRepository;
    private readonly IMemberRepository _memberRepository;
    private readonly IAreaRepository _areaRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPanchayatRepository _panchayatRepository;

    public UpdateDisputeRequestHandler(IDisputeRequestRepository disputeRequestRepository, IMemberRepository memberRepository,
        IAreaRepository areaRepository, IUserRepository userRepository,
        IMembershipPeriodRepository membershipPeriodRepository,
        IPanchayatRepository panchayatRepository)
    {
        _disputeRequestRepository = disputeRequestRepository;
        _memberRepository = memberRepository;
        _areaRepository = areaRepository;
        _userRepository = userRepository;
        _panchayatRepository = panchayatRepository;
    }

    public async Task HandleAsync(UpdateDisputeRequest command)
    {
        var disputeRequest = await _disputeRequestRepository.GetByIdAsync(command.Id);

        if (disputeRequest is null)
        {
            throw new DisputeRequestNotFoundException(command.Id);
        }
        var agent = await _userRepository.GetByIdAsync((Guid)command.SubmittedBy);

        if (agent is null)
        {
            throw new AgentNotFoundException(command.SubmittedBy);
        }

        var member = await _memberRepository.GetByIdAsync(disputeRequest.MemberId);
        
        if (member is null)
        {
            throw new MemberNotFoundException(disputeRequest.MemberId);
        }
        
        var area = await _areaRepository.GetByIdAsync(command.ToAreaId);
        
        if (area is null)
        {
            throw new AreaNotFoundException(command.ToAreaId);
        }
        
        var applicableAreas = await _areaRepository.GetByStateIdAsync((Guid)agent.StateId);

        var findArea = applicableAreas.FirstOrDefault(x => x.Id == command.ToAreaId);
 
        if (findArea is null)
        {
            throw new NotAuthorizedToCreateMemberForThisAreaException();
        }

        var panchayat = await _panchayatRepository.GetByIdAsync(command.ToPanchayatId);

        if (panchayat is null)
        {
            throw new InvalidPanchayatException();
        }

        
        var contract = new UpdateDisputeRequestContract
        {
            ToStateId = (Guid)agent.StateId,
            ToAreaId = command.ToAreaId,
            ToDistrictId = (Guid)agent.DistrictId,
            ToMandalamId = panchayat.MandalamId,
            ToPanchayatId = command.ToPanchayatId,
            Reason = command.Reason,
        };

        disputeRequest.Update(contract);
        await _disputeRequestRepository.UpdateAsync(disputeRequest);
    }
}