﻿using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Members;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.Members;

internal sealed class GetMemberByIdHandler : IQueryHandler<GetMemberById, MemberDto>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetMemberByIdHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<MemberDto> HandleAsync(GetMemberById query)
    {
        var memberId = query.MemberId;
        var member = await _dbContext.Members
            .Include(x => x.Profession)
            .Include(x => x.Qualification)
            .Include(x => x.Mandalam).ThenInclude(x => x.District)
            .Include(x => x.Panchayat)
            .Include(x => x.Area).ThenInclude(x => x.State)
            .Include(x => x.MembershipPeriod)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == memberId);

        var result = member?.AsDto();

        if (member?.AddressInDistrictId is not null)
        {
            var addressInDistrict =
                await _dbContext.Districts.SingleOrDefaultAsync(x =>
                    x.Id == ((Guid) member.AddressInDistrictId));
            if (addressInDistrict is not null)
            {
                result.AddressInDistrict = addressInDistrict.AsDto();
            }
        }

        if (member?.AddressInMandalamId is not null)
        {
            var addressInMandalam =
                await _dbContext.Mandalams.SingleOrDefaultAsync(x =>
                    x.Id == (Guid) member.AddressInMandalamId);
            if (addressInMandalam is not null)
            {
                result.AddressInMandalam = addressInMandalam.AsDto();
            }
        }
        
        if (member?.AddressInPanchayatId is not null)
        {
            var addressInPanchayat =
                await _dbContext.Panchayats.SingleOrDefaultAsync(x =>
                    x.Id == (Guid) member.AddressInPanchayatId);
            if (addressInPanchayat is not null)
            {
                result.AddressInPanchayat = addressInPanchayat.AsDto();
            }
        }

        return result;
    }
}