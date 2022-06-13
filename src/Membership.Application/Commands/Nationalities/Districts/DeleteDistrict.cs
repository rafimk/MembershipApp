using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Nationalities.Districts;

public record DeleteDistrict() : ICommand
{
    public Guid? DistrictId { get; set;}
}