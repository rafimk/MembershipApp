using Membership.Application.Abstractions;
using Membership.Application.DTO.Users;
using Membership.Application.Queries.Pagination;

namespace Membership.Application.Queries.Users;

public record GetUsersByRole : IQuery<PaginatedResult<UserDto>>
{
    public Guid? UserId { get; set; }
    public int? SearchType { get; set; }
    public string SearchString { get; set; }
    public int? PageIndex { get; set; } = 1;
    public int? PageSize { get; set; } = 10;
}