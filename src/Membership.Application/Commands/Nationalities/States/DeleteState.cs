using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Nationalities.States;

public record DeleteState() : ICommand
{
    public Guid Id { get; set;}
}