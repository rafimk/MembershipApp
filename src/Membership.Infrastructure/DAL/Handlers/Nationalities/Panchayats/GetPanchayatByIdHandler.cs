using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;
using Membership.Application.Queries.Nationalities.Panchayats;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Nationalities.Panchayats;

internal sealed class GetPanchayatByIdHandler : IQueryHandler<GetPanchayatById, PanchayatDto>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetPanchayatByIdHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<PanchayatDto> HandleAsync(GetPanchayatById query)
    {
        var panchayatId = query.PanchayatId;
        var panchayat = await _dbContext.Panchayats
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == panchayatId);

        return panchayat?.AsDto();
    }
}