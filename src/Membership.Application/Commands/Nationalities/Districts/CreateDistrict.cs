using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Nationalities.Districs;

public record CreateDistrict() : ICommand
{
    public string Name { get; set; }
}