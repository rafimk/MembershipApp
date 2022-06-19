﻿using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Members;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.Members;

internal sealed class GetMemberByIdHandler : IQueryHandler<GetMemberById, MemberDto>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetMemberByIdHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<MemberDto> HandleAsync(GetMemberById query)
    {
        var memberId = new GenericId(query.MemberId);
        var member = await _dbContext.Members
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == memberId);

        return member?.AsDto();
    }
}