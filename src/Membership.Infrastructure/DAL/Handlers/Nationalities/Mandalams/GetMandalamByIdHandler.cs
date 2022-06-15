using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;
using Membership.Application.Queries.Nationalities.Mandalams;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Nationalities.Mandalams;

internal sealed class GetMandalamByIdHandler : IQueryHandler<GetMandalamById, MandalamDto>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetMandalamByIdHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<MandalamDto> HandleAsync(GetMandalamById query)
    {
        var mandalamId = new GenericId(query.MandalamId);
        var mandalam = await _dbContext.Mandalams
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == mandalamId);

        return mandalam?.AsDto();
    }
}