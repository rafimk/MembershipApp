using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Professions;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.Professions;

internal sealed class GetProfessionByIdHandler : IQueryHandler<GetProfessionById, ProfessionDto>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetProfessionByIdHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<ProfessionDto> HandleAsync(GetProfessionById query)
    {
        var professionId = query.ProfessionId;
        var profession = await _dbContext.Professions
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == professionId);

        return profession?.AsDto();
    }
}