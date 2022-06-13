namespace Membership.Application.DTO.Nationalities;

public class PanchayatDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public GenericId MandalamId { get; set; }
    public MandalamDto Mandalam { get; set; }
    public string Prefix { get; set; }
    public DateTime CreatedAt { get; set; }
}