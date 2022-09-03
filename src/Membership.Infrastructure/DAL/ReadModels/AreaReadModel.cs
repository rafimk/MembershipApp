namespace Membership.Infrastructure.DAL.ReadModels;

public sealed class AreaReadModel
{
    public Guid Id { get; private set; }
    public string Name{ get; private set; }
    public Guid StateId { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime CreatedAt { get; private set; }
}