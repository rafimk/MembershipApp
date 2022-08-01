using Membership.Application.Abstractions;
using Membership.Application.DTO.Commons;
using Membership.Application.Exceptions.Users;
using Membership.Application.Queries.Commons;
using Membership.Core.DomainServices.Users;
using Membership.Core.Repositories.Nationalities;
using Membership.Core.Repositories.Users;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Commons;

internal sealed class GetMyLookupsHandler : IQueryHandler<GetMyLookups, MyLookupsDto>
{
    private readonly MembershipDbContext _dbContext;
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;
    private readonly IStateRepository _stateRepository;
    private readonly IDistrictRepository _districtRepository;

    public GetMyLookupsHandler(MembershipDbContext dbContext, IUserService userService,
         IUserRepository userRepository, IStateRepository stateRepository, IDistrictRepository districtRepository)
    {
        _dbContext = dbContext;
        _userService = userService;
        _userRepository = userRepository;
        _stateRepository = stateRepository;
        _districtRepository = districtRepository;
    }

    public async Task<MyLookupsDto> HandleAsync(GetMyLookups query)
    {
        var userInfo = await _userRepository.GetByIdAsync(query.UserId);
        
        var roles = await _userService.GetApplicableUserRolesAsync(userInfo.Role, query.UserId);
        
        if (userInfo is null)
        {
            throw new UserNotFoundException(query.UserId);
        }
        
        var lookupsDto = new MyLookupsDto
        {
            CascadeData = null,
            CascadeTitle = null,
            Areas = null,
            Panchayats = null,
            Qualifications = null,
            Professions = null,
            ApplicableUserRole = roles,
            CanDisputeCommittee = true
        };
        
        if (roles.Contains("state-admin") || roles.Contains("monitoring-officer"))
        {
            var cascadeData = await  _dbContext.States
                .OrderBy(x => x.Name)
                .AsNoTracking()
                .Select(x => x.AsCascadeDto())
                .ToListAsync();
            lookupsDto.CascadeData = cascadeData;
            lookupsDto.CascadeTitle = "State";
        }
        
        if (roles.Contains("district-admin"))
        {
            var cascadeData = await _dbContext.Districts
                .OrderBy(x => x.Name)
                .AsNoTracking()
                .Select(x => x.AsCascadeDto())
                .ToListAsync();
            lookupsDto.CascadeData = cascadeData;
            lookupsDto.CascadeTitle = "District";
        }
        
        if (roles.Contains("mandalam-agent"))
        {
            var cascadeData = await _dbContext.Mandalams
                .OrderBy(x => x.Name)
                .AsNoTracking()
                .Where(x => x.DistrictId == new GenericId((Guid)userInfo.CascadeId))
                .Select(x => x.AsCascadeDto())
                .ToListAsync();
            lookupsDto.CascadeData = cascadeData;
            lookupsDto.CascadeTitle = "Mandalam";
            
            var userDistrict = await _districtRepository.GetByIdAsync(userInfo.CascadeId);

            if (userDistrict is not null)
            {
                lookupsDto.DistrictsName = userDistrict.Name;
            }
        }

        if (userInfo.Role == UserRole.MandalamAgent() || userInfo.Role == UserRole.DistrictAgent())
        {
            var areas = await _dbContext.Areas
                .OrderBy(x => x.Name)
                .Include(x => x.State)
                .AsNoTracking()
                .Where(x => x.StateId == new GenericId((Guid)userInfo.StateId))
                .Select(x => x.AsDto())
                .ToListAsync();
            
            var panchayaths = await _dbContext.Panchayats
                .OrderBy(x => x.Name)
                .Include(x => x.Mandalam)
                .AsNoTracking()
                .Where(x => x.MandalamId == new GenericId((Guid)userInfo.CascadeId))
                .Select(x => x.AsDto())
                .ToListAsync();
            
            var qualifications = await _dbContext.Qualifications
                .OrderBy(x => x.Name)
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .Select(x => x.AsDto())
                .ToListAsync();
            
            var professions = await _dbContext.Professions
                .OrderBy(x => x.Name)
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .Select(x => x.AsDto())
                .ToListAsync();
            
            var registeredOrganizations = await _dbContext.RegisteredOrganizations
                .OrderBy(x => x.Name)
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .Select(x => x.AsDto())
                .ToListAsync();
            
            var welfareSchemes = await _dbContext.WelfareSchemes
                .OrderBy(x => x.Name)
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .Select(x => x.AsDto())
                .ToListAsync();
            
            var districts = await _dbContext.Districts
                .OrderBy(x => x.Name)
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .Select(x => x.AsDto())
                .ToListAsync();
            
            var membershipPeriod = await _dbContext.MembershipPeriods
                .AsNoTracking()
                .FirstAsync(x => x.IsActive);
            lookupsDto.CascadeTitle = userInfo.CascadeName;
            
            lookupsDto.District = districts;

            if (userInfo.Role == UserRole.MandalamAgent())
            {
                var userMandalam = await _dbContext.Mandalams
                    .Include(x => x.District)
                    .SingleOrDefaultAsync(x => x.Id == new GenericId((Guid)userInfo.CascadeId));

                if (userMandalam is not null)
                {
                    lookupsDto.DistrictsName = userMandalam.District?.Name;
                    lookupsDto.AgentDistrictId = userMandalam.DistrictId;
                    lookupsDto.AgentMandalamId = userInfo.CascadeId;
                }
            }

            if (userInfo.Role == UserRole.DistrictAgent())
            {
                var cascadeData = await _dbContext.Mandalams
                    .OrderBy(x => x.Name)
                    .AsNoTracking()
                    .Where(x => x.DistrictId == new GenericId((Guid) userInfo.CascadeId))
                    .Select(x => x.AsCascadeDto())
                    .ToListAsync();
                lookupsDto.CascadeData = cascadeData;
                lookupsDto.CascadeTitle = "Mandalam";
                
                var userDistrict = await _districtRepository.GetByIdAsync(userInfo.CascadeId);

                if (userDistrict is not null)
                {
                    lookupsDto.DistrictsName = userDistrict.Name;
                    lookupsDto.AgentDistrictId = userDistrict.Id;
                }
            }

            lookupsDto.Areas = areas;
            lookupsDto.Panchayats = panchayaths;
            lookupsDto.Qualifications = qualifications;
            lookupsDto.Professions = professions;
            lookupsDto.RegisteredOrganizations = registeredOrganizations;
            lookupsDto.WelfareSchemes = welfareSchemes;
            lookupsDto.MembershipPeriod = membershipPeriod?.AsDto();
            lookupsDto.CanDisputeCommittee = true;
        }
        else if (userInfo.Role == UserRole.DisputeCommittee())
        {
            lookupsDto.CanDisputeCommittee = false;
        }

        if (userInfo.StateId is null)
        {
            return lookupsDto;
        }
        
        var userSate = await _stateRepository.GetByIdAsync(userInfo.StateId);

        if (userSate is not null)
        {
            lookupsDto.StateName = userSate.Name;
        }

        return lookupsDto;
    }
}