using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Memberships.Qualifications;

public record CreateQualification() : ICommand
{
    public string Name{ get; set; }
}