using Membership.Application.Abstractions;
using Membership.Application.DTO.Users;
using Membership.Application.Queries.Users;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Users;

internal sealed class GetUserByMobileHandler : IQueryHandler<GetUserByMobile, UserDto>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetUserByMobileHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<UserDto> HandleAsync(GetUserByMobile query)
    {
        var user = await _dbContext.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Email == query.UserMobile);

        return user?.AsDto();
    }
}