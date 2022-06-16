using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;
using Membership.Application.Queries.Membership.Qualifications;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Membership.Qualifications;

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