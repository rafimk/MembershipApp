using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Members;
using Membership.Core.Repositories.Users;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.Members;

internal sealed class GetMembersAgentsByRoleHandler : IQueryHandler<GetMembersAgentsByRole, IEnumerable<AgentListDto>>
{
    private readonly MembershipReadDbContext _dbContext;
    private readonly IUserRepository _userRepository;

    public GetMembersAgentsByRoleHandler(MembershipReadDbContext dbContext, IUserRepository userRepository)
    {
        _dbContext = dbContext;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<AgentListDto>> HandleAsync(GetMembersAgentsByRole query)
    {
        var user = await _userRepository.GetByIdAsync((Guid)query.UserId);

        if (user is null)
        {
            return new List<AgentListDto>();
        }

        var dbQuery = _dbContext.Members
            .Include(x => x.Agent)
            .AsNoTracking()
            .AsQueryable();
        
        if (user.Role == UserRole.DistrictAgent())
        {
            var districtAgentDistrictId = (Guid)user.CascadeId;
            var districtAgentStateId = (Guid)user.StateId;

            dbQuery = dbQuery.Where(x => x.MandalamId == districtAgentDistrictId &&
                                         x.StateId == districtAgentStateId);

        }

        if (user.Role == UserRole.MandalamAgent())
        {
            var agentmandalamId = (Guid) user.CascadeId;
            var agentStateId = (Guid) user.StateId;

            dbQuery = dbQuery.Where(x => x.MandalamId == agentmandalamId &&
                                         x.StateId == agentStateId);
        }

        var result =await dbQuery.ToListAsync();

        var agentListDto = result.GroupBy(x => x.AgentId)
                .Select(x =>  x.First())
                .Select(x => new AgentListDto { Id = x.AgentId, Name = x.Agent.FullName});

        return agentListDto;
    }
}