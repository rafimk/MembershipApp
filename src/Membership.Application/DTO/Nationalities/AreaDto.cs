namespace Membership.Application.DTO.Nationalities;

public class AreaDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public StateDto State { get; set; }
    public DateTime CreatedAt { get; set; }
}