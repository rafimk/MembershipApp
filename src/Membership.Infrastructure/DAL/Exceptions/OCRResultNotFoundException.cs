using Membership.Core.Exceptions;

namespace Membership.Infrastructure.DAL.Exceptions;

public class OCRResultNotFoundException : CustomException
{
    public Guid Id { get; }
    
    public OCRResultNotFoundException(Guid id) : base($"OCR result no found for the Id : {id}.")
    {
        Id = id;
    }
}