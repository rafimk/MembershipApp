using Membership.Application.Abstractions;
using Membership.Application.DTO.Commons;
using Membership.Application.DTO.Nationalities;
using Membership.Application.Queries.Commons;
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
            .AsNoTracking()
            .Select(x => x.AsDto())
            .ToListAsync();

        var districts = await _dbContext.Districts
            .AsNoTracking()
            .Select(x => x.AsDto())
            .ToListAsync();
       
        var states = await  _dbContext.States
            .AsNoTracking()
            .Select(x => x.AsDto())
            .ToListAsync();
        
        var professions = await _dbContext.Professions
            .AsNoTracking()
            .Select(x => x.AsDto())
            .ToListAsync();
        
        var qualifications = await _dbContext.Qualifications
            .AsNoTracking()
            .Select(x => x.AsDto())
            .ToListAsync();

        var membershipPeriod = await _dbContext.MembershipPeriods
            .AsNoTracking()
            .Select(x => x.AsDto())
            .FirstAsync(x => x.IsActive);
        
        var lookupsDto = new LookupsDto
        {
            Areas = areas,
            Districts = districts,
            States = states,
            Professions = professions,
            Qualifications = qualifications,
            MembershipPeriod = membershipPeriod
        };

        return lookupsDto;
    }
}