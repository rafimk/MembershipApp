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
        
        if (user.Role != UserRole.MandalamAgent() && user.Role != UserRole.DistrictAgent() && user.Role != UserRole.DisputeCommittee())
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
                    (x.ToStateId == districtAgentStateId || x.FromStateId == districtAgentStateId));
                break;
            }
            case "mandalam-agent":
            {
                var mandalamId = (Guid)user.CascadeId;

                dbQuery = dbQuery.Where(x => (x.ToMandalamId == mandalamId || x.FromMandalamId == mandalamId)) 
                                 .OrderByDescending(x => x.SubmittedDate);
                break;
            }
            case "dispute-committee":
            {
                var stateId = (Guid)user.StateId;

                dbQuery = dbQuery.Where(x => (x.FromStateId == stateId || x.ToStateId == stateId));
                break;
            }
            case "central-dispute-admin":
            {
                dbQuery = dbQuery.Where(x => (x.FromStateId != x.ToStateId));
                break;
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
            }
        }
        
        dbQuery = dbQuery.OrderByDescending(x => x.SubmittedDate);
        
        var result = await dbQuery.GetPaged<DisputeRequestReadModel>((int)query.PageIndex, (int)query.PageSize);

        var paginatedResult = new PaginatedResult<DisputeRequestListDto>(result.Results.Select(x => x.AsDto()), result.RowCount, result.CurrentPage, (int)result.PageSize);
        
        if (user.Role == UserRole.DisputeCommittee())
        {
            foreach (var item in paginatedResult.Items)
            {
                if (item.FromState.Id == user.StateId && item.FromState.Id == item.ToState.Id)
                {
                    item.IsCanApprove = true;
                }
            }
        } else if (user.Role == UserRole.CentralDisputeAdmin())
        {
            foreach (var item in paginatedResult.Items)
            {
                if (item.FromState.Id != item.ToState.Id)
                {
                    item.IsCanApprove = true;
                }
            }
        }

        return paginatedResult;

    }
}