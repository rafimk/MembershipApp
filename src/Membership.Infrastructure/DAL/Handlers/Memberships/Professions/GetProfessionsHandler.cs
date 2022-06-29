using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Professions;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.Professions;

internal sealed class GetProfessionsHandler : IQueryHandler<GetProfessions, IEnumerable<ProfessionDto>>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetProfessionsHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<IEnumerable<ProfessionDto>> HandleAsync(GetProfessions query)
        => await _dbContext.Professions
            .OrderBy(x => x.Name)
            .Where(x => !x.IsDeleted)
            .AsNoTracking()
            .Select(x => x.AsDto())
            .ToListAsync();
}