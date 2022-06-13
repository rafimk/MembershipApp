using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Nationalities.Panchayats;

public record DeletePanchayat() : ICommand
{
    public Guid? PanchayatId { get; set;}
}