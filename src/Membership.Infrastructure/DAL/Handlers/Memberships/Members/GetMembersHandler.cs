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
            .AsNoTracking()
            .Select(x => x.AsDto())
            .Take(100)
            .ToListAsync();
    }
}