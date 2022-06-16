using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Qualifications;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.Qualifications;

internal sealed class GetQualificationByIdHandler : IQueryHandler<GetQualificationById, QualificationDto>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetQualificationByIdHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<QualificationDto> HandleAsync(GetQualificationById query)
    {
        var qualificationId = new GenericId(query.QualificationId);
        var qualification = await _dbContext.Qualifications
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == qualificationId);

        return qualification?.AsDto();
    }
}