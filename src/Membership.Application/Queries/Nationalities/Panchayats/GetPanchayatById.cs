using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;

namespace Membership.Application.Queries.Nationalities.Panchayats;

public class GetPanchayatById : IQuery<PanchayatDto>
{
    public Guid PanchayatId { get; set; }
}