using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.RegisteredOrganizations;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.RegisteredOrganizations;

internal sealed class GetRegisteredOrganizationByIdHandler : IQueryHandler<GetRegisteredOrganizationById, RegisteredOrganizationDto>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetRegisteredOrganizationByIdHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<RegisteredOrganizationDto> HandleAsync(GetRegisteredOrganizationById query)
    {
        var registeredOrganizationId = new GenericId(query.RegisteredOrganizationId);
        var registeredOrganization = await _dbContext.RegisteredOrganizations
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == registeredOrganizationId);

        return registeredOrganization?.AsDto();
    }
}