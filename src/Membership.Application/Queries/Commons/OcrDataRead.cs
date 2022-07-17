using Membership.Application.Abstractions;
using Membership.Application.DTO.Commons;
using Membership.Core.Entities.Commons;

namespace Membership.Application.Queries.Commons;

public class OcrDataRead : IQuery<OcrDataDto>
{
    public Guid? FrontPageId { get; set; }
    public Guid? LastPageId { get; set; }
    public Guid? UserId { get; set; }
    public string FilePath { get; set; }
}