using Membership.Application.Abstractions;
using Membership.Application.Exceptions.Memberships;
using Membership.Application.Exceptions.Nationalities;
using Membership.Application.Exceptions.Users;
using Membership.Core.Abstractions;
using Membership.Core.Consts;
using Membership.Core.Contracts.Memberships;
using Membership.Core.Entities.Memberships.Members;
using Membership.Core.Exceptions.Memberships;
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
        if (IsAgeLessThan18Years(command.DateOfBirth))
        {
            throw new AgeLessThan18YearsException();
        }
        
        var membershipPeriod = await _membershipPeriodRepository.GetActivePeriodAsync();
 
        if (membershipPeriod is null)
        {
            throw new ThereIsNoActiveMembershipPeriodAvailable();
        }
        
        if (command.EmiratesIdExpiry <= membershipPeriod.RegistrationStarted)
        {
            throw new InvalidEmiratesIdExpiryDate();
        }
        
        var agent = await _userRepository.GetByIdAsync((Guid)command.AgentId);

        if (agent is null)
        {
            throw new AgentNotFoundException(command.AgentId);
        }

        if (agent.Role == UserRole.MandalamAgent())
        {
            command.MandalamId = (Guid)agent?.CascadeId;
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
        
        var applicableAreas = await _areaRepository.GetByStateIdAsync((Guid)agent.StateId);

        var findArea = applicableAreas.FirstOrDefault(x => x.Id == command.AreaId);
 
        if (findArea is null)
        {
            throw new NotAuthorizedToCreateMemberForThisAreaException();
        }

        var membership = Member.Create(new CreateMemberContract
        {
            Id = (Guid)command.Id,
            MembershipId = membershipId,
            FullName = command.FullName,
            EmiratesIdNumber = command.EmiratesIdNumber,
            EmiratesIdExpiry = command.EmiratesIdExpiry,
            EmiratesIdFrontPage = command.EmiratesIdFrontPage,
            EmiratesIdLastPage = command.EmiratesIdLastPage,
            DateOfBirth = command.DateOfBirth,
            MobileNumber = command.MobileNumber,
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
            AddressInDistrictId = command.AddressInDistrictId,
            AddressInMandalamId = command.AddressInMandalamId,
            AddressInPanchayatId = command.AddressInPanchayatId,
            PasswordHash = command.PasswordHash,
            StateId = (Guid)agent.StateId,
            AreaId = (Guid)command.AreaId,
            DistrictId = (Guid)agent.DistrictId,
            MandalamId = command.MandalamId,
            PanchayatId = command.PanchayatId,
            RegisteredOrganizationId = command.RegisteredOrganizationId,
            WelfareSchemeId = command.WelfareSchemeId,
            MembershipPeriodId = membershipPeriod.Id,
            Status = MemberStatus.Draft,
            CreatedAt = _clock.Current(),
            CreatedBy = (Guid)command.AgentId,
            CardNumber = command.CardNumber,
            ManuallyEntered = command.ManuallyEntered,
            AgentId = command.AgentId,
            MembershipNoPrefix = area.State?.Prefix
        });
        
        await _memberRepository.AddAsync(membership);
    }
    
    private bool IsAgeLessThan18Years(DateTime birthDate)
    {
        if (DateTime.Now.Year - birthDate.Year > 18)
        {
            return false;
        }
        else if (DateTime.Now.Year - birthDate.Year < 18)
        {
            return true;
        }
        else //if (DateTime.Now.Year - birthDate.Year == 18)
        {
            if (birthDate.DayOfYear < DateTime.Now.DayOfYear)
            {
                return false;
            }
            else if (birthDate.DayOfYear > DateTime.Now.DayOfYear)
            {
                return true;
            }
            else //if (birthDate.DayOfYear == DateTime.Now.DayOfYear)
            {
                return false;
            }
        }
    }
}