using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Memberships.Professions;

public record DeleteProfession() : ICommand
{
    public Guid? ProfessionId { get; set;}
}