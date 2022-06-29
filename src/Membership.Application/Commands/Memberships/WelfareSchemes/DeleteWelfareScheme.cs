using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Memberships.WelfareSchemes;

public record DeleteWelfareScheme() : ICommand
{
    public Guid? WelfareSchemeId { get; set;}
}