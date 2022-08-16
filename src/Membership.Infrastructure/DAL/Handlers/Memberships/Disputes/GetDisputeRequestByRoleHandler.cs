using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Disputes;
using Membership.Core.Repositories.Users;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.Disputes;

internal sealed class GetDisputeRequestByRoleHandler : IQueryHandler<GetDisputeRequestByRole, IEnumerable<DisputeRequestDto>>
{
    private readonly MembershipDbContext _dbContext;
    private readonly IUserRepository _userRepository;

    public GetDisputeRequestByRoleHandler(MembershipDbContext dbContext, IUserRepository userRepository)
    {
        _dbContext = dbContext;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<DisputeRequestDto>> HandleAsync(GetDisputeRequestByRole query)
    {
        var user = await _userRepository.GetByIdAsync(query.UserId);

        if (user is null)
        {
            return null;
        }
        
        if (user.Role != UserRole.MandalamAgent())
        {
            return null;
        }
        
        if (user.Role == UserRole.DistrictAgent())
        {
            var districtAgentDistrictId = (Guid)user.CascadeId;
            var districtAgentStateId = (Guid)user.StateId;
            
            return await _dbContext.DisputeRequests
                .Include(x => x.ProposedArea).ThenInclude(x => x.State)
                .Include(x => x.ProposedMandalam).ThenInclude(x => x.District)
                .Include(x => x.ProposedPanchayat).ThenInclude(x => x.Mandalam)
                .AsNoTracking()
                .Where(x => x.DistrictId == districtAgentDistrictId &&
                            x.StateId == districtAgentStateId)
                .Select(x => x.AsDto())
                .ToListAsync();
        }
        
        var mandalamId = (Guid)user.CascadeId;
        
        return await _dbContext.DisputeRequests
            .Include(x => x.ProposedArea).ThenInclude(x => x.State)
            .Include(x => x.ProposedMandalam).ThenInclude(x => x.District)
            .Include(x => x.ProposedPanchayat).ThenInclude(x => x.Mandalam)
            .AsNoTracking()
            .Where(x => x.ProposedMandalamId == mandalamId)
            .Select(x => x.AsDto())
            .ToListAsync();
    }
}