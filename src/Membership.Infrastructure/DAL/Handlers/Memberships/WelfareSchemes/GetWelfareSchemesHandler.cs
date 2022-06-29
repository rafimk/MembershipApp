using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.DTO.Nationalities;
using Membership.Application.Queries.Memberships;
using Membership.Application.Queries.Memberships.Qualifications;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.Qualifications;

internal sealed class GetWelfareSchemesHandler : IQueryHandler<GetRegisteredOrganizations, IEnumerable<WelfareSchemeDto>>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetWelfareSchemesHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<IEnumerable<WelfareSchemeDto>> HandleAsync(GetWelfareSchemes query)
        => await _dbContext.WelfareSchemes
            .Where(x => !x.IsDeleted)
            .OrderBy(x => x.Name);
            .AsNoTracking()
            .Select(x => x.AsDto())
            .ToListAsync();
}