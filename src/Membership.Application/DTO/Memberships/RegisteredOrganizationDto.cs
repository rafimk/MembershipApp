namespace Membership.Application.DTO.Memberships;

public class RegisteredOrganizationDto
{
    public Guid? Id { get; set; }
    public string Name{ get; set; }
    public DateTime CreatedAt { get; set; }
}