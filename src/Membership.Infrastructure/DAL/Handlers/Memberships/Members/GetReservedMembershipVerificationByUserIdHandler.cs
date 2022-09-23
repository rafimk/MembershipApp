using Membership.Application.Abstractions;
using Membership.Application.Queries.Memberships.Members;
using Membership.Core.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.Members;

internal sealed class GetReservedMembershipVerificationByUserIdHandler : IQueryHandler<GetReservedMembershipVerificationByUserId, Guid?>
{
    private readonly IUserRepository _userRepository;
    private readonly MembershipDbContext _dbContext;

    public GetReservedMembershipVerificationByUserIdHandler(IUserRepository userRepository, MembershipDbContext dbContext)
    {
        _userRepository = userRepository;
        _dbContext = dbContext;
    }

    public async Task<Guid?> HandleAsync(GetReservedMembershipVerificationByUserId query)
    {
        var user = await _userRepository.GetByIdAsync((Guid)query.UserId);

        if (user is null)
        {
            return null;
        }

        var result = await _dbContext.MembershipVerifications
            .Include(x => x.Member)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.VerifiedUserId == query.UserId && x.VerifiedAt == null);

        return result?.Id;
    }
}