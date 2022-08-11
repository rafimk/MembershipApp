using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Members;
using Membership.Core.Repositories.Users;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.Members;

internal sealed class GetMembersByRoleHandler : IQueryHandler<GetMembersByRole, IEnumerable<MemberDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly MembershipDbContext _dbContext;

    public GetMembersByRoleHandler(IUserRepository userRepository, MembershipDbContext dbContext)
    {
        _userRepository = userRepository;
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<MemberDto>> HandleAsync(GetMembersByRole query)
    {
        var user = await _userRepository.GetByIdAsync(query.UserId);

        if (user is null)
        {
            return null;
        }
        
        if (user.Role != UserRole.MandalamAgent() && user.Role != UserRole.DistrictAgent())
        {
            return new List<MemberDto>();
        }

        if (user.Role == UserRole.DistrictAgent())
        {
            var districtAgentDistrictId = (Guid)user.CascadeId;
            var districtAgentStateId = (Guid)user.StateId;
            
            return await _dbContext.Members
                .OrderBy(x => x.FullName)
                .Include(x => x.Mandalam).ThenInclude(x => x.District)
                .Include(x => x.Panchayat)
                .Include(x => x.Area).ThenInclude(x => x.State)
                .Include(x => x.MembershipPeriod)
                .AsNoTracking()
                .Where(x => x.DistrictId == districtAgentDistrictId && 
                            x.StateId == districtAgentStateId)
                .OrderBy(x => x.MembershipId)
                .Select(x => x.AsDto())
                .ToListAsync();
        }
        
        var agentmandalamId = (Guid)user.CascadeId;
        var agentStateId = (Guid)user.StateId;
        
        return await _dbContext.Members
            .OrderBy(x => x.FullName)
            .Include(x => x.Mandalam).ThenInclude(x => x.District)
            .Include(x => x.Panchayat)
            .Include(x => x.Area).ThenInclude(x => x.State)
            .Include(x => x.MembershipPeriod)
            .AsNoTracking()
            .Where(x => x.MandalamId == agentmandalamId &&
                        x.StateId == agentStateId)
            .Select(x => x.AsDto())
            .ToListAsync();
    }
}