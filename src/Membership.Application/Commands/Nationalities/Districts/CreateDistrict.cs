using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Nationalities.Districts;

public record CreateDistrict() : ICommand
{
    public string Name { get; set; }
}