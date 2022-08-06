using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.WelfareSchemes;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.WelfareSchemes;

internal sealed class GetWelfareSchemeByIdHandler : IQueryHandler<GetWelfareSchemeById, WelfareSchemeDto>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetWelfareSchemeByIdHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<WelfareSchemeDto> HandleAsync(GetWelfareSchemeById query)
    {
        var welfareSchemeId = query.WelfareSchemeId;
        var welfareScheme = await _dbContext.WelfareSchemes
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == welfareSchemeId);

        return welfareScheme?.AsDto();
    }
}