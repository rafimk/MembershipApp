using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Nationalities.Districts;

public record CreateDistrict() : ICommand
{
    public Guid DistrictId { get; set; }
    public string Name { get; set; }
}