using Membership.Application.Abstractions;
using Membership.Application.DTO.Commons;

namespace Membership.Application.Queries.Commons;

public class GetMyLookups : IQuery<MyLookupsDto>
{
    public Guid? UserId { get; set; }
}