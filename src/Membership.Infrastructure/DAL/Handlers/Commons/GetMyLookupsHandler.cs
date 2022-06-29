using Membership.Application.Abstractions;
using Membership.Application.DTO.Commons;
using Membership.Application.Exceptions.Users;
using Membership.Application.Queries.Commons;
using Membership.Core.DomainServices.Users;
using Membership.Core.Repositories.Users;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Commons;

internal sealed class GetMyLookupsHandler : IQueryHandler<GetMyLookups, MyLookupsDto>
{
    private readonly MembershipDbContext _dbContext;
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;

    public GetMyLookupsHandler(MembershipDbContext dbContext, 
        IUserService userService, IUserRepository userRepository)
    {
        _dbContext = dbContext;
        _userService = userService;
        _userRepository = userRepository;
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
        
        if (roles.Contains("state-admin"))
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
            lookupsDto.CascadeTitle = "District";
        }

        if (userInfo.Role == UserRole.MandalamAgent())
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
            
            var membershipPeriod = await _dbContext.MembershipPeriods
                .AsNoTracking()
                .FirstAsync(x => x.IsActive);

            lookupsDto.Areas = areas;
            lookupsDto.Panchayats = panchayaths;
            lookupsDto.Qualifications = qualifications;
            lookupsDto.Professions = professions;
            lookupsDto.RegisteredOrganizations = registeredOrganizations;
            lookupsDto.WelfareSchemes = welfareSchemes;
            lookupsDto.MembershipPeriod = membershipPeriod?.AsDto();
            lookupsDto.CanDisputeCommittee = false;
        }
        else if (userInfo.Role == UserRole.DisputeCommittee())
        {
            lookupsDto.CanDisputeCommittee = false;
        }
        
        return lookupsDto;
    }
}