using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Qualifications;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.Qualifications;

internal sealed class GetWelfareSchemeByIdHandler : IQueryHandler<GetRegisteredOrganization, WelfareSchemeDto>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetWelfareSchemeByIdHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<WelfareSchemeDto> HandleAsync(GetRegisteredOrganization query)
    {
        var welfareSchemeId = new GenericId(query.WelfareSchemeId);
        var welfareScheme = await _dbContext.WelfareSchemes
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == welfareSchemeId);

        return registeredOrganization?.AsDto();
    }
}