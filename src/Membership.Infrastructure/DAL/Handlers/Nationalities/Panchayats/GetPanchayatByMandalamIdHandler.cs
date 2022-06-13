using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;
using Membership.Application.Queries.Nationalities;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Nationalities.Panchayats;

internal sealed class GetPanchayatByMandalamIdHandler : IQueryHandler<GetPanchayatByMandalamId, PanchayatDto>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetPanchayatByMandalamIdHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<PanchayatDto> HandleAsync(GetPanchayatByMandalamId query)
    {
        var mandalamId = new GenericId(query.MandalamId);
        var panchayats = await _dbContext.Panchayats
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.MandalamId == mandalamId);

        return panchayats?.AsDto();
    }
}