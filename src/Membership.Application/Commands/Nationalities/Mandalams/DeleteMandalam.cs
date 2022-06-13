using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Nationalities.Mandalams;

public record DeleteMandalam() : ICommand
{
    public Guid MandalamId { get; set;}
}