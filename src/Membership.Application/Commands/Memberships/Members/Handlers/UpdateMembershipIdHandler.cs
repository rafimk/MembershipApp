using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Memberships;
using Membership.Application.Exceptions.Nationalities;
using Membership.Core.Repositories.Memberships;
using Membership.Core.Repositories.Nationalities;

namespace Membership.Application.Commands.Memberships.Members.Handlers;

internal sealed class UpdateMembershipIdHandler : ICommandHandler<UpdateMembershipId>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IAreaRepository _areaRepository;

    public UpdateMembershipIdHandler(IMemberRepository memberRepository, IAreaRepository areaRepository)
    {
        _memberRepository = memberRepository;
       _areaRepository = areaRepository;
    }

    public async Task HandleAsync(UpdateMembershipId command)
    {
        var membership = await _memberRepository.GetByIdAsync(command.Id);
        
        if (membership is null)
        {
            throw new MemberNotFoundException(command.Id);
        }
        
        var area = await _areaRepository.GetByIdAsync(membership.AreaId);
        
        if (area is null)
        {
            throw new AreaNotFoundException(area.Id);
        }

        var membershipId = $"{area.State?.Prefix.Trim()}{membership.SequenceNo.ToString("D6")}";

        membership.UpdateMembershipId(membershipId);
        await _memberRepository.UpdateAsync(membership);
    }
}