using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Memberships.Professions;

public record CreateProfession() : ICommand
{
    public Guid ProfessionId { get; set; }
    public string Name{ get; set; }
}