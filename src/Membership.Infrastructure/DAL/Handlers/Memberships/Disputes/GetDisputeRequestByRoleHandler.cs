using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Disputes;
using Membership.Application.Queries.Pagination;
using Membership.Core.Consts;
using Membership.Core.Repositories.Users;
using Membership.Core.ValueObjects;
using Membership.Infrastructure.DAL.ReadModels;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.Disputes;

internal sealed class GetDisputeRequestByRoleHandler : IQueryHandler<GetDisputeRequestByRole, PaginatedResult<DisputeRequestListDto>>
{
    private readonly MembershipReadDbContext _dbContext;
    private readonly IUserRepository _userRepository;

    public GetDisputeRequestByRoleHandler(MembershipReadDbContext dbContext, IUserRepository userRepository)
    {
        _dbContext = dbContext;
        _userRepository = userRepository;
    }

    public async Task<PaginatedResult<DisputeRequestListDto>> HandleAsync(GetDisputeRequestByRole query)
    {
        var user = await _userRepository.GetByIdAsync((Guid)query.UserId);

        if (user is null)
        {
            return new PaginatedResult<DisputeRequestListDto>(new List<DisputeRequestListDto>(), 0, (int)query.PageIndex, (int)query.PageSize);
        }
        
        if (user.Role != UserRole.MandalamAgent() && user.Role != UserRole.DistrictAgent())
        {
            return new PaginatedResult<DisputeRequestListDto>(new List<DisputeRequestListDto>(), 0, (int)query.PageIndex, (int)query.PageSize);
        }

        var dbQuery =  _dbContext.DisputeRequests
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
                .AsQueryable();
        
            // .OrderByDescending(x => x.SubmittedDate)
            // .Select(x => x.AsDto())
            // .ToListAsync();

        switch (@user.Role.ToString())
        {
            case "district-agent":
            {
                var districtAgentDistrictId = (Guid)user.CascadeId;
                var districtAgentStateId = (Guid)user.StateId;

                dbQuery = dbQuery.Where(x =>
                    (x.ToDistrictId == districtAgentDistrictId || x.FromDistrictId == districtAgentDistrictId) &&
                    (x.ToStateId == districtAgentStateId || x.FromStateId == districtAgentStateId) &&
                    x.Status == DisputeStatus.Pending);
                break;
            }
            case "mandalam-agent":
            {
                var mandalamId = (Guid)user.CascadeId;

                dbQuery = dbQuery.Where(x => (x.ToMandalamId == mandalamId || x.FromMandalamId == mandalamId) && 
                                             x.Status == DisputeStatus.Pending)
                                 .OrderByDescending(x => x.SubmittedDate);
                break;
            }
            case "dispute-committee":
            {
                var stateId = (Guid)user.StateId;

                dbQuery = dbQuery.Where(x => (x.FromStateId == stateId || x.ToStateId == stateId))
                    .OrderByDescending(x => x.SubmittedDate);
                break;
                // foreach (var item in results)
                // {
                //     if (item.FromState.Id == stateId)
                //     {
                //         item.IsCanApprove = true;
                //     }
                // }
                //
                // return results;
            }
            default:
            {
                return new PaginatedResult<DisputeRequestListDto>(new List<DisputeRequestListDto>(), 0, (int)query.PageIndex, (int)query.PageSize);
            }
        }
        
        if (query.SearchString?.Trim().Length > 0)
        {
            switch (query.SearchType)
            {
                case 1:
                {
                    dbQuery = dbQuery.Where(x => x.Member.FullName.ToLower().Contains(query.SearchString.ToLower()));
                    break;
                }
                case 2:
                {
                    dbQuery = dbQuery.Where(x => x.Member.MobileNumber.Contains(query.SearchString));
                    break;
                }
                case 3:
                {
                    dbQuery = dbQuery.Where(x => x.FromState.Name.ToLower().Contains(query.SearchString.ToLower()));
                    break;
                }
                case 4:
                {
                    dbQuery = dbQuery.Where(x => x.ToState.Name.Contains(query.SearchString));
                    break;
                }
                default:
                {
                    dbQuery = dbQuery.OrderByDescending(x => x.SubmittedDate);
                    break;
                }
            }
        }
        
        var result = await dbQuery.GetPaged<DisputeRequestReadModel>((int)query.PageIndex, (int)query.PageSize);
        
        return new PaginatedResult<DisputeRequestListDto>(result.Results.Select(x => x.AsDto()), result.RowCount, result.CurrentPage, (int)result.PageSize);
        
    }
}