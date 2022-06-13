namespace MMS.Domain.Entities.Contracts;

public record UserCreateContract()
{
    public GenericId Id { get; set; }
    public FullName FullName { get; set; }
    public Email Email { get; set; }
    public MobileNumber MobileNumber { get; set; } 
    public MobileNumber AlternativeContactNumber { get; set; }
    public string Designation { get; set; }
    public string PasswordHash { get; set; }
    public UserRole Role { get; set; }
    public Guid? CascadeId { get; set; }
    public DateTime CreatedAt { get; set; }
}