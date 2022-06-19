using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Members;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.Members;

internal sealed class GetMembersByMandalamIdHandler : IQueryHandler<GetMembersByMandalamId, IEnumerable<MemberDto>>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetMembersByMandalamIdHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<IEnumerable<MemberDto>> HandleAsync(GetMembersByMandalamId query)
    {
        var mandalamId = new GenericId(query.MandalamId);
        return await _dbContext.Members
            .AsNoTracking()
            .Where(x => x.MandalamId == mandalamId)
            .Select(x => x.AsDto())
            .ToListAsync();
    }
}