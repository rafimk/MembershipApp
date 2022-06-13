namespace Membership.Application.DTO.Nationalities;

public class MandalamDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid DistrictId { get; set; }
    public DistrictDto District { get; set; }
    public string Prefix { get; set; }
    public DateTime CreatedAt { get; set; }
}