using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Nationalities.Mandalams;

public record CreateMandalam() : ICommand
{
    public Guid MandalamId { get; set; }
    public string Name{ get; set; }
    public Guid DistrictId { get; set; }
}