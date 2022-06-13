using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Nationalities.Areas;

public record DeleteArea(): ICommand
{
    public Guid? AreaId { get; set;}
}