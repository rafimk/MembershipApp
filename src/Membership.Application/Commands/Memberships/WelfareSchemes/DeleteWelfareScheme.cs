using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Memberships.WelfareSchemes;

public record DeleteWelfareSchemes() : ICommand
{
    public Guid? WelfareSchemeId { get; set;}
}