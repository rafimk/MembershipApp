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
    private readonly IUserService _userService;

    public CreateMemberHandler(IMemberRepository memberRepository, IAreaRepository areaRepository,
        IUserService userService)
    {
        _memberRepository = memberRepository;
        _areaRepository = areaRepository;
        _userService = userService;
    }

    public async Task HandleAsync(CreateMember command)
    {
        if (await _memberRepository.GetByEmailAsync(command.Email) is not null)
        {
            throw new EmailAlreadyInUseException(command.Email);
        }

        if (await _memberRepository.GetByEmiratesIdAsync(command.EmiratesId) is not null)
        {
            throw new EmiratesIdAlreadyInUseException(command.EmiratesId);
        }

        var area = await _areaRepository.GetByIdAsync(command.AreaId);

        if (area is null)
        {
            throw new AreaNotFoundException(command.AreaId);
        }

        var membershipId = _memberRepository.GenerateMembershipId(area.State?.Prefix);

        if (await _memberRepository.GetByMemberIdAsync(command.Email) is not null)
        {
            throw new EmailAlreadyInUseException(command.Email);
        }

        var agent = _userService.gGetByIdAsync(command.AgentId);

        if (agent is null)
        {
            throw new NotAuthorizedRoleException(command.Role);
        }

        var applicableAreas = _areaRepository.GetByStateIdAsync(agent.stateId).Select(x => x.Id);

        if (!applicableAreas.Contains(command.AreaId))
        {
            throw new NotAuthorizedToCreateMemberForThisAreaException();
        }

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
            MandalamId = agent.CascadeId,
            PanchayatId = command.PanchayatId,
            IsMemberOfAnyIndianRegisteredOrganization = command.IsMemberOfAnyIndianRegisteredOrganization,
            IsKMCCWelfareScheme = command.IsKMCCWelfareScheme,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = Guid.NewGuid()
        });

        await _memberRepository.AddAsync(membership);
    }
}