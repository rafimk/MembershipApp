using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Pagination;

namespace Membership.Application.Queries.Memberships.Disputes;

public class GetDisputeRequestByRole : IQuery<PaginatedResult<DisputeRequestListDto>>
{
    public Guid? UserId { get; set; }
    public int? SearchType { get; set; }
    public string SearchString { get; set; }
    public int? PageIndex { get; set; } = 1;
    public int? PageSize { get; set; } = 10;
}