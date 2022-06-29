using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.DTO.Nationalities;
using Membership.Application.Queries.Memberships;
using Membership.Application.Queries.Memberships.Qualifications;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.Qualifications;

internal sealed class GetRegisteredOrganizationsHandler : IQueryHandler<GetRegisteredOrganizations, IEnumerable<RegisteredOrganizationDto>>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetRegisteredOrganizationsHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<IEnumerable<RegisteredOrganizationDto>> HandleAsync(GetRegisteredOrganizations query)
        => await _dbContext.RegisteredOrganizations
            .Where(x => !x.IsDeleted)
            .OrderBy(x => x.Name);
            .AsNoTracking()
            .Select(x => x.AsDto())
            .ToListAsync();
}