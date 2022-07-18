using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Members;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.Members;

internal sealed class IsDisputeHandler : IQueryHandler<IsDispute, IsDisputeDto>
{
    private readonly MembershipDbContext _dbContext;
    
    public IsDisputeHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<IsDisputeDto> HandleAsync(IsDispute query)
    {
        var member = await _dbContext.Members
            .Include(x => x.Profession)
            .Include(x => x.Qualification)
            .Include(x => x.Mandalam).ThenInclude(x => x.District)
            .Include(x => x.Panchayat)
            .Include(x => x.Area).ThenInclude(x => x.State)
            .Include(x => x.MembershipPeriod)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.EmiratesIdNumber == query.EmiratesIdNumber);

        if (member is not null)
        {
            return new IsDisputeDto
            {
                IsDispute = true,
                Member = member?.AsDto()
            };
        }
        return new IsDisputeDto
        {
            IsDispute = false,
            Member = null
        };
    }
}