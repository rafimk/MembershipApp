using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;
using Membership.Application.Queries.Nationalities;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Users;

internal sealed class GetUsersHandler : IQueryHandler<GetUsers, IEnumerable<UserDto>>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetUsersHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<IEnumerable<UserDto>> HandleAsync(GetUsers query)
        => await _dbContext.Users
            .AsNoTracking()
            .Select(x => x.AsDto())
            .ToListAsync();
}