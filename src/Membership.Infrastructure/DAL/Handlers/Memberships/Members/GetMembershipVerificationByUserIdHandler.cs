using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Members;
using Membership.Core.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.Members;

internal sealed class GetMembershipVerificationByUserIdHandler : IQueryHandler<GetMembershipVerificationByUserId, MembershipVerificationDto>
{
    private readonly IUserRepository _userRepository;
    private readonly MembershipDbContext _dbContext;

    public GetMembershipVerificationByUserIdHandler(IUserRepository userRepository, MembershipDbContext dbContext)
    {
        _userRepository = userRepository;
        _dbContext = dbContext;
    }

    public async Task<MembershipVerificationDto> HandleAsync(GetMembershipVerificationByUserId query)
    {
        var user = await _userRepository.GetByIdAsync((Guid)query.UserId);

        if (user is null)
        {
            return null;
        }

        var result = await _dbContext.MembershipVerifications
            .Include(x => x.Member)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == query.Id && x.VerifiedUserId == query.UserId);

        return result?.AsDto();
    }
}