using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Nationalities.States;

public record UpdateState() : ICommand
{
    public Guid? StateId { get; set;}
    public string Name{ get; set; }
}