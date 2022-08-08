using Membership.Core.Consts;

namespace Membership.Infrastructure.OCR;

public class OcrData
{
    public string IdNumber { get; set; }
    public string Name { get; set;}
    public DateTime? DateofBirth { get; set;}
    public DateTime? ExpiryDate { get; set; }
    public string CardNumber { get; set;}
    public CardType CardType { get; set;}
    public Gender Gender { get; set; }
    public bool ErrorOccurred { get; set; }
}