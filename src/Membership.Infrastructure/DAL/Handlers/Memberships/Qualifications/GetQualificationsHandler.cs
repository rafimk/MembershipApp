using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.DTO.Nationalities;
using Membership.Application.Queries.Memberships;
using Membership.Application.Queries.Memberships.Qualifications;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.Qualifications;

internal sealed class GetQualificationsHandler : IQueryHandler<GetQualifications, IEnumerable<QualificationDto>>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetQualificationsHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<IEnumerable<QualificationDto>> HandleAsync(GetQualifications query)
        => await _dbContext.Qualifications
            .Where(x => !x.IsDeleted)
            .AsNoTracking()
            .Select(x => x.AsDto())
            .ToListAsync();
}