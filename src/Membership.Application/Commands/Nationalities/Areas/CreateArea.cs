using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Nationalities.Areas;

public record CreateArea() : ICommand
{
    public Guid AreaId { get; set; }
    public string Name { get; set; }
    public Guid StateId { get; set; }
}