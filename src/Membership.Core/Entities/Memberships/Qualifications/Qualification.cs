﻿using Membership.Core.ValueObjects;

namespace Membership.Core.Entities.Memberships.Qualifications;

public class Qualification 
{
    public GenericId Id { get; private set; }
    public string Name{ get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Qualification(GenericId id, string name, bool isDeleted, DateTime createdAt)
    {
        Id = id;
        Name = name;
        IsDeleted = isDeleted;
        CreatedAt = createdAt;
    }

    private Qualification()
    {
    }
    
    public static Qualification Create(GenericId id, string name, DateTime createdAt)
        => new(id, name, false, createdAt);

    public void Update(string name)
    {
        Name = name;
    }
    
    public void Delete()
    {
        IsDeleted = true;
    }
}