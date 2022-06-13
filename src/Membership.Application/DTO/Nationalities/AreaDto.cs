using Membership.Core.ValueObjects;

namespace Membership.Application.DTO.Nationalities;

public class AreaDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public GenericId StateId { get; set; }
    public StateDto State { get; set; }
    public string Prefix { get; set; }
    public DateTime CreatedAt { get; set; }
}