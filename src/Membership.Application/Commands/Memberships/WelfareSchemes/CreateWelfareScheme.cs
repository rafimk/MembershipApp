using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Memberships.WelfareSchemes;

public record CreateWelfareScheme() : ICommand
{
    public Guid WelfareSchemeId { get; set; }
    public string Name{ get; set; }
}