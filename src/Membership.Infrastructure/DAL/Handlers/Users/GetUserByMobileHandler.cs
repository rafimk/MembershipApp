using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;
using Membership.Application.Queries.Nationalities;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Users;

internal sealed class GetUserByMobileHandler : IQueryHandler<GetUserByMobile, UserDto>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetUserByMobileHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<UserDto> HandleAsync(GetUserByMobile query)
    {
        var userMobile = new GenericId(query.UserMobile);
        var user = await _dbContext.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Email == userMobile);

        return user?.AsDto();
    }
}