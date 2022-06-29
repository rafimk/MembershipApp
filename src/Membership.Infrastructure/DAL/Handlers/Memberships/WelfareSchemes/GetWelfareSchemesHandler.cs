using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.WelfareSchemes;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.WelfareSchemes;

internal sealed class GetWelfareSchemesHandler : IQueryHandler<GetWelfareSchemes, IEnumerable<WelfareSchemeDto>>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetWelfareSchemesHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<IEnumerable<WelfareSchemeDto>> HandleAsync(GetWelfareSchemes query)
        => await _dbContext.WelfareSchemes
            .Where(x => !x.IsDeleted)
            .OrderBy(x => x.Name)
            .AsNoTracking()
            .Select(x => x.AsDto())
            .ToListAsync();
}