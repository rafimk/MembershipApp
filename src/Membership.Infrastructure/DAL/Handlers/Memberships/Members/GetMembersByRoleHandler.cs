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
        
        if (user.Role != UserRole.MandalamAgent())
        {
            return null;
        }
        
        var mandalamId = new GenericId((Guid)user.CascadeId);
        return await _dbContext.Members
            .OrderBy(x => x.FullName)
            .Include(x => x.Profession)
            .Include(x => x.Qualification)
            .Include(x => x.Mandalam).ThenInclude(x => x.District)
            .Include(x => x.Panchayat)
            .Include(x => x.Area).ThenInclude(x => x.State)
            .Include(x => x.MembershipPeriod)
            .AsNoTracking()
            .Where(x => x.MandalamId == mandalamId)
            .Select(x => x.AsDto())
            .ToListAsync();
    }
}