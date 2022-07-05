using Membership.Core.Exceptions;

namespace Membership.Infrastructure.DAL.Exceptions;

public class OcrResultNotFoundException : CustomException
{
    public Guid Id { get; }
    
    public OcrResultNotFoundException(Guid id) : base($"OCR result no found for the Id : {id}.")
    {
        Id = id;
    }
}