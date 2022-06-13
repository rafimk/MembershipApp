using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Memberships.Qualifications;

public record DeleteQualification() : ICommand
{
    public Guid? QualificationId { get; set;}
}