using System.ComponentModel.DataAnnotations;

namespace Membership.Shared.Deduplication;

public class DeduplicationModel
{
    [Key]
    public string MessageId { get; set; }
    public DateTime ProcessedAt { get; set; }
}