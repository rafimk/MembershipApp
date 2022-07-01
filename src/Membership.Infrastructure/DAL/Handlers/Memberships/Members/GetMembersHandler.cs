using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Members;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.Members;

internal sealed class GetMembersHandler : IQueryHandler<GetMembers, IEnumerable<MemberDto>>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetMembersHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<IEnumerable<MemberDto>> HandleAsync(GetMembers query)
    {
        return await _dbContext.Members
            .Include(x => x.Profession)
            .Include(x => x.Qualification)
            .Include(x => x.Mandalam)
            .Include(x => x.Panchayat)
            .Include(x => x.Area).ThenInclude(x => x.State)
            .Include(x => x.MembershipPeriod)
            .AsNoTracking()
            .Select(x => x.AsDto())
            .Take(1000)
            .ToListAsync();
    }
}