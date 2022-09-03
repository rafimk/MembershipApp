using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Members;
using Membership.Application.Queries.Pagination;
using Membership.Core.Repositories.Users;
using Membership.Core.ValueObjects;
using Membership.Infrastructure.DAL.ReadModels;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.Members;

internal sealed class GetMembersByRoleHandler : IQueryHandler<GetMembersByRole, PaginatedResult<MemberListDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly MembershipReadDbContext _dbContext;

    public GetMembersByRoleHandler(IUserRepository userRepository, MembershipReadDbContext dbContext)
    {
        _userRepository = userRepository;
        _dbContext = dbContext;
    }

    public async Task<PaginatedResult<MemberListDto>> HandleAsync(GetMembersByRole query)
    {
        var user = await _userRepository.GetByIdAsync((Guid)query.UserId);

        if (user is null)
        {
            return new PaginatedResult<MemberListDto>(new List<MemberListDto>(), 0, (int)query.PageIndex, (int)query.PageSize);
        }
        
        if (user.Role != UserRole.MandalamAgent() && user.Role != UserRole.DistrictAgent())
        {
            return new PaginatedResult<MemberListDto>(new List<MemberListDto>(), 0, (int)query.PageIndex, (int)query.PageSize);
        }

        var dbQuery = _dbContext.Members
            .OrderBy(x => x.FullName)
            .Include(x => x.Mandalam)
            .Include(x => x.Panchayat)
            .Include(x => x.Area)
            .AsNoTracking()
            .AsQueryable();

        if (user.Role == UserRole.DistrictAgent())
        {
            var districtAgentDistrictId = (Guid)user.CascadeId;
            var districtAgentStateId = (Guid)user.StateId;

            dbQuery = dbQuery.Where(x => x.DistrictId == districtAgentDistrictId &&
                                    x.StateId == districtAgentStateId);

        }

        if (user.Role == UserRole.MandalamAgent())
        {

            var agentmandalamId = (Guid) user.CascadeId;
            var agentStateId = (Guid) user.StateId;

            dbQuery = dbQuery.Where(x => x.MandalamId == agentmandalamId &&
                                         x.StateId == agentStateId);
        }

        if (query.SearchString?.Trim().Length > 0)
        {
            switch (query.SearchType)
            {
                case 1:
                {
                    var filter = new FullName(query.SearchString);
                    dbQuery = dbQuery.Where(x => x.FullName.Contains(filter));
                    break;
                }
                case 2:
                {
                    dbQuery = dbQuery.Where(x => x.MobileNumber.Contains(query.SearchString));
                    break;
                }
                case 3:
                {
                    dbQuery = dbQuery.Where(x => x.Panchayat.Name.Contains(query.SearchString));
                    break;
                }
                case 4:
                {
                    dbQuery = dbQuery.Where(x => x.EmiratesIdNumber.Contains(query.SearchString));
                    break;
                }
                case 5:
                {
                    if (user.Role == UserRole.MandalamAgent())
                    {
                        var agentmandalamId = (Guid) user.CascadeId;
                        var agentStateId = (Guid) user.StateId;

                        var searchUser =
                            await _dbContext.Users.FirstAsync(x => x.FullName.Contains(query.SearchString) &&
                                                                   x.StateId == agentStateId &&
                                                                   x.MandalamId == agentmandalamId);

                        if (searchUser is not null)
                        {
                            dbQuery = dbQuery.Where(x => x.CreatedBy == searchUser.Id);
                        }
                    }
                    else if (user.Role == UserRole.DistrictAgent())
                    {
                        var agentStateId = (Guid)user.StateId;

                        var searchUser =
                            await _dbContext.Users.FirstAsync(x => x.FullName.Contains(query.SearchString) &&
                                                                   x.StateId == agentStateId);

                        if (searchUser is not null)
                        {
                            dbQuery = dbQuery.Where(x => x.CreatedBy == searchUser.Id);
                        }
                    }

                    break;
                }
                case 6:
                {
                    dbQuery = dbQuery.Where(x => x.Mandalam.Name.Contains(query.SearchString));
                    break;
                }
                default:
                {
                    dbQuery = dbQuery.OrderByDescending(x => x.CreatedAt);
                    break;
                }
            }
        }
        
        var result = await dbQuery.GetPaged<MemberReadModel>((int)query.PageIndex, (int)query.PageSize);
        
        return new PaginatedResult<MemberListDto>(result.Results.Select(x => x.AsDto()), result.RowCount, result.CurrentPage, (int)result.PageSize);
    }
}