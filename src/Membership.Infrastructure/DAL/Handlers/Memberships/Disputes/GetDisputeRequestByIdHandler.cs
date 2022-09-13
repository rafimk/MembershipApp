using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Disputes;
using Membership.Core.Repositories.Users;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.Disputes;

internal sealed class GetDisputeRequestByIdHandler : IQueryHandler<GetDisputeRequestById, DisputeRequestDto>
{
    private readonly MembershipDbContext _dbContext;
    private readonly IUserRepository _userRepository;

    public GetDisputeRequestByIdHandler(MembershipDbContext dbContext, IUserRepository userRepository)
    {
        _dbContext = dbContext;
        _userRepository = userRepository;
    }
    
    public async Task<DisputeRequestDto> HandleAsync(GetDisputeRequestById query)
    {
        var requestId = query.RequestId;

        var disputeRequest = await _dbContext.DisputeRequests
            .Include(x => x.Member)
            .Include(x => x.FromState)
            .Include(x => x.FromArea)
            .Include(x => x.FromDistrict)
            .Include(x => x.FromMandalam)
            .Include(x => x.FromPanchayat)
            .Include(x => x.ToState)
            .Include(x => x.ToArea)
            .Include(x => x.ToDistrict)
            .Include(x => x.ToMandalam)
            .Include(x => x.ToPanchayat)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == requestId);

        var result = disputeRequest?.AsDtoWithMember();
        
        var user = await _userRepository.GetByIdAsync((Guid)query.UserId);

        if (user is not null && result is not null)
        {
            if (result.FromState.Id == user.StateId && user.Role == UserRole.DisputeCommittee())
            {
                result.IsCanApprove = true;
            }
        }

        return result;
    }
}