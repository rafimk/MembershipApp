using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Memberships.Professions;

public record UpdateProfession() : ICommand
{
    public Guid? ProfessionId { get; set;}
    public string Name{ get; set; }
}