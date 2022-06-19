using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Nationalities;
using Membership.Core.Consts;
using Membership.Core.Contracts.Memberships;
using Membership.Core.Entities.Memberships.Members;
using Membership.Core.Repositories.Memberships;
using Membership.Core.Repositories.Nationalities;

namespace Membership.Application.Commands.Memberships.Members.Handlers;

internal sealed class CreateMemberHandler : ICommandHandler<CreateMember>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IAreaRepository _areaRepository;

    public CreateMemberHandler(IMemberRepository memberRepository, IAreaRepository areaRepository)
    {
        _memberRepository = memberRepository;
        _areaRepository = areaRepository;
    }

    public async Task HandleAsync(CreateMember command)
    {
        var area = await _areaRepository.GetByIdAsync(command.AreaId);

        if (area is null)
        {
            throw new AreaNotFoundException(command.AreaId);
        }
        
        var membershipId = _memberRepository.GenerateMembershipId(area.State?.Prefix);
        var membership = Member.Create(new CreateMemberContract
        {
            Id = command.Id,
            MembershipId = membershipId,
            FullName = command.FullName,
            EmiratesIdNumber = command.EmiratesIdNumber,
            EmiratesIdExpiry = command.EmiratesIdExpiry,
            EmiratesIdFrontPage = command.EmiratesIdFrontPage,
            EmiratesIdLastPage = command.EmiratesIdLastPage,
            DateOfBirth = command.DateOfBirth,
            MobileNumber = command.MobileNumber,
            AlternativeContactNumber = command.AlternativeContactNumber,
            Email = command.Email,
            PassportNumber = command.PassportNumber,
            PassportExpiry = command.PassportExpiry,
            ProfessionId = command.ProfessionId,
            QualificationId = command.QualificationId,
            BloodGroup = (BloodGroup)command.BloodGroup,
            HouseName = command.HouseName,
            AddressInIndia = command.AddressInIndia,
            PasswordHash = command.PasswordHash,
            AreaId = command.AreaId,
            MandalamId = command.MandalamId,
            IsMemberOfAnyIndianRegisteredOrganization = command.IsMemberOfAnyIndianRegisteredOrganization,
            IsKMCCWelfareScheme = command.IsKMCCWelfareScheme,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = Guid.NewGuid()
        });
        await _memberRepository.AddAsync(membership);
    }
}