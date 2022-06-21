using Membership.Application.Abstractions;
using Membership.Application.DTO.Commons;
using Membership.Application.Exceptions.Users;
using Membership.Application.Queries.Commons;
using Membership.Application.Queries.Users;
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
                .Where(x => x.DistrictId.ToString() == userInfo.CascadeId.ToString())
                .Select(x => x.AsCascadeDto())
                .ToListAsync();
            lookupsDto.CascadeData = cascadeData;
            lookupsDto.CascadeTitle = "District";
        }

        if (userInfo.Role == UserRole.MandalamAgent())
        {
            var areas = await _dbContext.Areas
                .AsNoTracking()
                .Where(x => x.StateId.ToString() == userInfo.StateId.ToString())
                .Select(x => x.AsDto())
                .ToListAsync();
            
            var panchayaths = await _dbContext.Panchayats
                .AsNoTracking()
                .Where(x => x.MandalamId.ToString() == userInfo.CascadeId.ToString())
                .Select(x => x.AsDto())
                .ToListAsync();

            lookupsDto.Areas = areas;
            lookupsDto.Panchayats = panchayaths;
        }
        
        return lookupsDto;
    }
}