﻿using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Members;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.Members;

internal sealed class GetMembersByAreaIdHandler : IQueryHandler<GetMembersByAreaId, IEnumerable<MemberDto>>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetMembersByAreaIdHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<IEnumerable<MemberDto>> HandleAsync(GetMembersByAreaId query)
    {
        var areaId = query.AreaId;
        return await _dbContext.Members
            .OrderBy(x => x.FullName)
            .Include(x => x.Profession)
            .Include(x => x.Qualification)
            .Include(x => x.Mandalam).ThenInclude(x => x.District)
            .Include(x => x.Panchayat)
            .Include(x => x.MembershipPeriod)
            .Include(x => x.Area).ThenInclude(x => x.State)
            .AsNoTracking()
            .Where(x => x.AreaId == areaId)
            .Select(x => x.AsDto())
            .ToListAsync();
    }
}