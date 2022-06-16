using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;
using Membership.Application.Queries.Memberships;
using Membership.Application.Queries.Memberships.Professions;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.Professions;

internal sealed class GetProfessionByIdHandler : IQueryHandler<GetProfessionById, ProfessionDto>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetProfessionByIdHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<ProfessionDto> HandleAsync(GetProfessionById query)
    {
        var professionId = new GenericId(query.professionId);
        var profession = await _dbContext.Professions
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == professionId);

        return profession?.AsDto();
    }
}