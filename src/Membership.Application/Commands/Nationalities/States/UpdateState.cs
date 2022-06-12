using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Nationalities.States;

public record UpdateState() : ICommand
{
    public Guid Id { get; set;}
    public string Name{ get; set; }
}