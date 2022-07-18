using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;

namespace Membership.Application.Queries.Memberships.Members;

public class IsDispute : IQuery<IsDisputeDto>
{
    public string EmiratesIdNumber { get; set; }
}