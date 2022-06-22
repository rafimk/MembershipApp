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
            ApplicableUserRole = roles
        };
        
        if (roles.Contains("state-admin"))
        {
            var cascadeData = await  _dbContext.States
                .AsNoTracking()
                .Select(x => x.AsCascadeDto())
                .ToListAsync();
            lookupsDto.CascadeData = cascadeData;
            lookupsDto.CascadeTitle = "State";
        }
        
        if (roles.Contains("district-admin"))
        {
            var cascadeData = await _dbContext.Districts
                .AsNoTracking()
                .Select(x => x.AsCascadeDto())
                .ToListAsync();
            lookupsDto.CascadeData = cascadeData;
            lookupsDto.CascadeTitle = "District";
        }
        
        if (roles.Contains("mandalam-agent"))
        {
            var cascadeData = await _dbContext.Mandalams
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
                .Include(x => x.State)
                .AsNoTracking()
                .Where(x => x.StateId == new GenericId((Guid)userInfo.StateId))
                .Select(x => x.AsDto())
                .ToListAsync();
            
            var panchayaths = await _dbContext.Panchayats
                .Include(x => x.Mandalam)
                .AsNoTracking()
                .Where(x => x.MandalamId == new GenericId((Guid)userInfo.CascadeId))
                .Select(x => x.AsDto())
                .ToListAsync();
            
            var qualifications = await _dbContext.Qualifications
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .Select(x => x.AsDto())
                .ToListAsync();
            
            var professions = await _dbContext.Professions
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .Select(x => x.AsDto())
                .ToListAsync();

            lookupsDto.Areas = areas;
            lookupsDto.Panchayats = panchayaths;
            lookupsDto.Qualifications = qualifications;
            lookupsDto.Professions = professions;
        }
        
        return lookupsDto;
    }
}