using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Nationalities.Panchayats;

public record CreatePanchayat() : ICommand
{
    public Guid PanchayatId { get; set; }
    public string Name{ get; set; }
    public Guid MandalamId { get; set; }
    public int Type { get; set; }
}