using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Memberships;
using Membership.Application.Exceptions.Nationalities;
using Membership.Application.Exceptions.Users;
using Membership.Core.Abstractions;
using Membership.Core.Consts;
using Membership.Core.Contracts.Memberships;
using Membership.Core.Entities.Memberships.Members;
using Membership.Core.Repositories.Memberships;
using Membership.Core.Repositories.Nationalities;
using Membership.Core.Repositories.Users;
using Membership.Core.ValueObjects;

namespace Membership.Application.Commands.Memberships.Members.Handlers;

internal sealed class CreateMemberHandler : ICommandHandler<CreateMember>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IAreaRepository _areaRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMembershipPeriodRepository _membershipPeriodRepository;
    private readonly IClock _clock;

    public CreateMemberHandler(IMemberRepository memberRepository, IAreaRepository areaRepository,
        IUserRepository userRepository, IMembershipPeriodRepository membershipPeriodRepository, IClock clock)
    {
        _memberRepository = memberRepository;
        _areaRepository = areaRepository;
        _userRepository = userRepository;
        _membershipPeriodRepository = membershipPeriodRepository;
        _clock = clock;
    }

    public async Task HandleAsync(CreateMember command)
    {
        var agent = await _userRepository.GetByIdAsync(command.AgentId);

        if (agent is null)
        {
            throw new AgentNotFoundException(command.AgentId);
        }
        
        if (await _memberRepository.GetByEmailAsync(command.Email) is not null)
        {
            throw new EmailAlreadyInUseException(command.Email);
        }
        
        if (await _memberRepository.GetByEmiratesIdAsync(command.EmiratesIdNumber) is not null)
        {
            throw new EmiratesIdNumberAlreadyInUseException(command.EmiratesIdNumber);
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

        var applicableAreas = await _areaRepository.GetByStateIdAsync(agent.StateId);

        var findArea = applicableAreas.FirstOrDefault(x => x.Id == new GenericId(command.AreaId));
 
        if (findArea is null)
        {
            throw new NotAuthorizedToCreateMemberForThisAreaException();
        }

        var membershipPeriod = await _membershipPeriodRepository.GetActivePeriodAsync();
 
        if (membershipPeriod is null)
        {
            throw new ThereIsNoActiveMembershipPeriodAvailable();
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
            PassportFrontPage = command.PassportFrontPage,
            PassportLastPage = command.PassportLastPage,
            ProfessionId = command.ProfessionId,
            QualificationId = command.QualificationId,
            BloodGroup = (BloodGroup)command.BloodGroup,
            Gender = (Gender)command.Gender,
            Photo = command.Photo,
            HouseName = command.HouseName,
            AddressInIndia = command.AddressInIndia,
            PasswordHash = command.PasswordHash,
            AreaId = command.AreaId,
            MandalamId = agent.CascadeId,
            PanchayatId = command.PanchayatId,
            RegisteredOrganizationId = command.RegisteredOrganizationId,
            WelfareSchemeId = command.WelfareSchemeId,
            MembershipPeriodId = membershipPeriod.Id,
            Status = MemberStatus.Draft,
            CreatedAt = _clock.Current(),
            CreatedBy = command.AgentId
        });
        
        await _memberRepository.AddAsync(membership);
    }
}