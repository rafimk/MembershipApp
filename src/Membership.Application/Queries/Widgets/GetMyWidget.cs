using Membership.Application.Abstractions;
using Membership.Application.DTO.Widgets;

namespace Membership.Application.Queries.Widgets;

public class GetMyWidget : IQuery<IEnumerable<WidgetDto>>
{
    public Guid? UserId { get; set; }
}