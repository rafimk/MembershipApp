using Membership.Application.Abstractions;
using Membership.Application.DTO.Commons;
using Membership.Application.Queries.Commons;
using Membership.Application.Queries.Users;
using Membership.Core.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Commons;

internal sealed class GetMyLookupsHandler : IQueryHandler<GetMyLookups, MyLookupsDto>
{
    private readonly MembershipDbContext _dbContext;
    private readonly IUserRepository _userRepository;
    private readonly IQueryHandler<GetApplicableUserRole, IEnumerable<string>> _getApplicableUserRoleHandler;

    public GetMyLookupsHandler(MembershipDbContext dbContext, 
        IQueryHandler<GetApplicableUserRole, IEnumerable<string>> getApplicableUserRoleHandler,
        IUserRepository userRepository)
    {
        _dbContext = dbContext;
        _getApplicableUserRoleHandler = getApplicableUserRoleHandler;
        _userRepository = userRepository;
    }

    public async Task<MyLookupsDto> HandleAsync(GetMyLookups query)
    {
        var roles = await _getApplicableUserRoleHandler.HandleAsync(new GetApplicableUserRole {UserId = query.UserId});

        var userInfo = await _userRepository.GetByIdAsync(query.UserId);
        
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
        return lookupsDto;
    }
}