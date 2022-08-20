using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Disputes;
using Membership.Core.Consts;
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
            return new List<DisputeRequestDto>();
        }

        switch (@user.Role.ToString())
        {
            case "district-agent":
            {
                var districtAgentDistrictId = (Guid)user.CascadeId;
                var districtAgentStateId = (Guid)user.StateId;
            
                return await _dbContext.DisputeRequests
                    .Include(x => x.Member)
                    .Include(x => x.FromState)
                    .Include(x => x.FromArea)
                    .Include(x => x.FromDistrict)
                    .Include(x => x.FromMandalam)
                    .Include(x => x.FromPanchayat)
                    .Include(x => x.ToState)
                    .Include(x => x.ToArea)
                    .Include(x => x.ToDistrict)
                    .Include(x => x.ToMandalam)
                    .Include(x => x.ToPanchayat)
                    .AsNoTracking()
                    .Where(x => (x.ToDistrictId == districtAgentDistrictId || x.FromDistrictId == districtAgentDistrictId) &&
                                (x.ToStateId == districtAgentStateId || x.FromStateId == districtAgentStateId) && 
                                x.Status == DisputeStatus.Pending)
                    .OrderByDescending(x => x.SubmittedDate)
                    .Select(x => x.AsDto())
                    .ToListAsync();
            }
            case "mandalam-agent":
            {
                var mandalamId = (Guid)user.CascadeId;
        
                return await _dbContext.DisputeRequests
                    .Include(x => x.Member)
                    .Include(x => x.FromState)
                    .Include(x => x.FromArea)
                    .Include(x => x.FromDistrict)
                    .Include(x => x.FromMandalam)
                    .Include(x => x.FromPanchayat)
                    .Include(x => x.ToState)
                    .Include(x => x.ToArea)
                    .Include(x => x.ToDistrict)
                    .Include(x => x.ToMandalam)
                    .Include(x => x.ToPanchayat)
                    .AsNoTracking()
                    .Where(x => (x.ToMandalamId == mandalamId || x.FromMandalamId == mandalamId)&&
                                x.Status == DisputeStatus.Pending)
                    .OrderByDescending(x => x.SubmittedDate)
                    .Select(x => x.AsDto())
                    .ToListAsync();
            }
            case "dispute-committee":
            {
                var stateId = (Guid)user.StateId;
           
                return await _dbContext.DisputeRequests
                    .Include(x => x.Member)
                    .Include(x => x.FromState)
                    .Include(x => x.FromArea)
                    .Include(x => x.FromDistrict)
                    .Include(x => x.FromMandalam)
                    .Include(x => x.FromPanchayat)
                    .Include(x => x.ToState)
                    .Include(x => x.ToArea)
                    .Include(x => x.ToDistrict)
                    .Include(x => x.ToMandalam)
                    .Include(x => x.ToPanchayat)
                    .AsNoTracking()
                    .Where(x => (x.ToStateId == stateId || x.ToStateId == stateId))
                    .OrderByDescending(x => x.SubmittedDate)
                    .Select(x => x.AsDto())
                    .ToListAsync();
            }
            default:
            {
                return new List<DisputeRequestDto>();
            }
        }
    }
}