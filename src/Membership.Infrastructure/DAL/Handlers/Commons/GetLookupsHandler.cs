using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;
using Membership.Application.Queries.Nationalities;
using Membership.Application.Queries.Nationalities.Areas;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Commons;

internal sealed class GetLookupsHandler : IQueryHandler<GetLookups, LookupsDto>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetLookupsHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<LookupsDto> HandleAsync(GetLookups query)
    {
        var areas = await _dbContext.Areas
            .Include(x => x.State)
            .Where(x => !x.IsDelete)
            .AsNoTracking()
            .Select(x => x.AsDto())
            .ToListAsync();

        var districts = await _dbContext.Districts
            .Where(x => !x.IsDelete)
            .AsNoTracking()
            .Select(x => x.AsDto())
            .ToListAsync();
        
        var mandalams = await _dbContext.Mandalams
            .Include(x => x.District)
            .Where(x => !x.IsDelete)
            .AsNoTracking()
            .Select(x => x.AsDto())
            .ToListAsync();
        
        var states = await states = _dbContext.States
            .Where(x => !x.IsDelete)
            .AsNoTracking()
            .Select(x => x.AsDto())
            .ToListAsync();
        
        var qualifications = await _dbContext.Qualifications
            .Where(x => !x.IsDelete)
            .AsNoTracking()
            .Select(x => x.AsDto())
            .ToListAsync();

         var membershipPeriod = await _dbContext.MembershipPeriodd
            .AsNoTracking()
            .FirstAsync(x => x.IsActive).AsDto();
        
        var lookupsDto = new LookupsDto
        {
            Areas = areas,
            Districts = districts,
            Mandalams = mandalams,
            States = states,
            Professions = professions,
            Qualifications = qualifications,
            MembershipPeriod = membershipPeriod
        };

        return lookupsDto;
    }
}