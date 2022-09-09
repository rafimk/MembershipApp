using Membership.Application.Abstractions;
using Membership.Application.DTO.Users;
using Membership.Application.Queries.Pagination;
using Membership.Application.Queries.Users;
using Membership.Core.DomainServices.Users;
using Membership.Core.Repositories.Users;
using Membership.Core.ValueObjects;
using Membership.Infrastructure.DAL.Handlers.Users.Policies;
using Membership.Infrastructure.DAL.ReadModels;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Users;

internal sealed class GetUsersByRoleHandler : IQueryHandler<GetUsersByRole, PaginatedResult<UserDto>>
{
    private readonly MembershipReadDbContext _dbContext;
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;
    private readonly IEnumerable<IUserDataRetrievePolicy> _policies;

    public GetUsersByRoleHandler(MembershipReadDbContext dbContext, IUserRepository userRepository, 
        IUserService userService, IEnumerable<IUserDataRetrievePolicy> policies)
    {
        _dbContext = dbContext;
        _userRepository = userRepository;
        _userService = userService;
        _policies = policies;
    }

    public async Task<PaginatedResult<UserDto>> HandleAsync(GetUsersByRole query)
    {
        var user = await _userRepository.GetByIdAsync((Guid)query.UserId);

        if (user is null)
        {
            return new PaginatedResult<UserDto>(new List<UserDto>(), 0, (int)query.PageIndex, (int)query.PageSize);
        }

        if (user.Role == UserRole.DisputeCommittee() ||
            user.Role == UserRole.DistrictAgent() ||
            user.Role == UserRole.MandalamAgent() ||
            user.Role == UserRole.MonitoringOfficer())
        {
            return new PaginatedResult<UserDto>(new List<UserDto>(), 0, (int)query.PageIndex, (int)query.PageSize);
        }

        var applicableUserRoles = await _userService.GetApplicableUserRolesAsync(user.Role, query.UserId);

        var dbQuery = _dbContext.Users
            .OrderBy(x => x.FullName)
            .AsNoTracking()
            .AsQueryable();


        if (user.Email != "admin@admin.com")
        {
            switch (user.Role.ToString().ToLower())
            {
                case "centralcommittee-admin":
                {
                    dbQuery = dbQuery.Where(x => applicableUserRoles.Contains(x.Role));
                    break;
                }
                case "state-admin":
                {
                    dbQuery = dbQuery.Where(x => applicableUserRoles.Contains(x.Role) && 
                                                 x.StateId == user.StateId);
                    break;
                }
                case "district-admin":
                {
                    dbQuery = dbQuery.Where(x => applicableUserRoles.Contains(x.Role) && 
                                                 x.StateId == user.StateId && x.DistrictId == user.DistrictId);
                    break;
                }
            }
        }
      
        
        if (query.SearchString?.Trim().Length > 0)
        {
            switch (query.SearchType)
            {
                case 1:
                {
                    dbQuery = dbQuery.Where(x => x.FullName.ToLower().Contains(query.SearchString.ToLower()));
                    break;
                }
                case 2:
                {
                    dbQuery = dbQuery.Where(x => x.MobileNumber.Contains(query.SearchString));
                    break;
                }
                case 3:
                {
                    dbQuery = dbQuery.Where(x => x.Email.ToLower().Contains(query.SearchString.ToLower()));
                    break;
                }
                default:
                {
                    dbQuery = dbQuery.OrderByDescending(x => x.CreatedAt);
                    break;
                }
            }
        }

        var result = await dbQuery.GetPaged<UserReadModel>((int)query.PageIndex, (int)query.PageSize);
        
        return new PaginatedResult<UserDto>(result.Results.Select(x => x.AsDto()), result.RowCount, result.CurrentPage, (int)result.PageSize);
 
    }
}