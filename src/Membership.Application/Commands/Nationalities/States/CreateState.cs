using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Nationalities.States;

public record CreateState() : ICommand
{
    public Guid? Id { get; set; }
    public string Name{ get; set; }
    public string Prefix { get; set; }
}