using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Nationalities.Areas;

public record UpdateArea() : ICommand
{
    public Guid? AreaId { get; set;}
    public string Name{ get; set; }
    public Guid StateId { get; set; }
    public string Prefix { get; set; }
}