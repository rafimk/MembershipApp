using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Members;
using Membership.Core.Repositories.Users;
using Membership.Core.ValueObjects;
using Membership.Infrastructure.DAL.ReadModels;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.Members;

internal sealed class
    GetMembersByRoleAsExcelHandeler : IQueryHandler<GetMembersByRoleAsExcel, IEnumerable<MemberListDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly MembershipReadDbContext _dbContext;

    public GetMembersByRoleAsExcelHandeler(IUserRepository userRepository, MembershipReadDbContext dbContext)
    {
        _userRepository = userRepository;
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<MemberListDto>> HandleAsync(GetMembersByRoleAsExcel query)
    {
        var user = await _userRepository.GetByIdAsync((Guid) query.UserId);

        if (user is null)
        {
            return new List<MemberListDto>();
        }

        if (user.Role != UserRole.MandalamAgent() && user.Role != UserRole.DistrictAgent())
        {
            return new List<MemberListDto>();
        }

        var dbQuery = _dbContext.Members
            .OrderBy(x => x.FullName)
            .Include(x => x.State)
            .Include(x => x.District)
            .Include(x => x.Mandalam)
            .Include(x => x.Panchayat)
            .Include(x => x.Area)
            .Include(x => x.Agent)
            .AsNoTracking()
            .AsQueryable();

        if (user.Role == UserRole.DistrictAgent())
        {
            var districtAgentDistrictId = (Guid) user.CascadeId;
            var districtAgentStateId = (Guid) user.StateId;

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
                    dbQuery = dbQuery.Where(x => x.Panchayat.Name.ToLower().Contains(query.SearchString.ToLower()));
                    break;
                }
                case 4:
                {
                    dbQuery = dbQuery.Where(x => x.EmiratesIdNumber.Contains(query.SearchString));
                    break;
                }
                case 5:
                {
                    dbQuery = dbQuery.Where(x => x.Agent.FullName.ToLower().Contains(query.SearchString.ToLower()));
                    break;
                }
                case 6:
                {
                    dbQuery = dbQuery.Where(x => x.Mandalam.Name.ToLower().Contains(query.SearchString.ToLower()));
                    break;
                }
            }
        }

        dbQuery = dbQuery.OrderBy(x => x.CreatedAt);

        var result = await dbQuery.Select(x => x.AsListDto()).ToListAsync();

        return result;
    }
}