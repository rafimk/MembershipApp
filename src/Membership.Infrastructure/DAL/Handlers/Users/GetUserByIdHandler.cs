using Membership.Application.Abstractions;
using Membership.Application.DTO.Users;
using Membership.Application.Queries.Users;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Users;

internal sealed class GetUserByIdHandler : IQueryHandler<GetUserById, UserDto>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetUserByIdHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<UserDto> HandleAsync(GetUserById query)
    {
        var userId = new GenericId(query.UserId);
        var user = await _dbContext.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == userId);

        return user?.AsDto();
    }
}