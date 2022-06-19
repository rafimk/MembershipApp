using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Members;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.Members;

internal sealed class GetMembersByAreaIdHandler : IQueryHandler<GetMembersByAreaId, IEnumerable<MemberDto>>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetMembersByAreaIdHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<IEnumerable<MemberDto>> HandleAsync(GetMembersByAreaId query)
    {
        var areaId = new GenericId(query.AreaId);
        return await _dbContext.Members
            .AsNoTracking()
            .Where(x => x.AreaId == areaId)
            .Select(x => x.AsDto())
            .ToListAsync();
    }
}