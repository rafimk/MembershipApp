using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Memberships.Professions;

public record CreateProfession() : ICommand
{
    public string Name{ get; set; }
}