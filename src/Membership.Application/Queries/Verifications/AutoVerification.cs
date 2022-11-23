﻿using Membership.Application.Abstractions;

namespace Membership.Application.Queries.Verifications;

public class AutoVerification : IQuery<bool>
{
    public DateTime ProcessDate { get; set; }
    public string FilePath { get; set; }
}