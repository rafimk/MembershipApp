namespace Membership.Infrastructure.DAL.ReadModels;

public sealed class StateReadModel
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Prefix { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime CreatedAt { get; private set; }
}