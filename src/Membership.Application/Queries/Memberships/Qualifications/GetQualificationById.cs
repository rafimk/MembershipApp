using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;

namespace Membership.Application.Queries.Memberships.Qualifications;

public class GetQualificationById : IQuery<QualificationDto>
{
    public Guid QualificationId { get; set; }
}