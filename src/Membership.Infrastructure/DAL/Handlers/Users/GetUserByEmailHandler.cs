using Membership.Application.Abstractions;
using Membership.Application.DTO.Users;
using Membership.Application.Queries.Users;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Users;

internal sealed class GetUserByEmailHandler : IQueryHandler<GetUserByEmail, UserDto>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetUserByEmailHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<UserDto> HandleAsync(GetUserByEmail query)
    {
        var user = await _dbContext.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Email == query.UserEmail);

        return user?.AsDto();
    }
}