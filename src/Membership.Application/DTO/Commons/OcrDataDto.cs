using Membership.Core.Consts;

namespace Membership.Application.DTO.Commons;

public class OcrDataDto
{
    public string IdNumber { get; set; }
    public string Name { get; set;}
    public DateTime? DateofBirth { get; set;}
    public DateTime? ExpiryDate { get; set; }
    public string CardNumber { get; set;}
    public int Gender { get; set;}
    public CardType CardType { get; set;}
    public bool IsDispute { get; set; } = false;
    public bool IsDuplicate { get; set; } = false;
    public OcrStatus Status { get; set; }
    public bool IsValidate { get; set; } = true;
    public string ErrorMessage { get; set; }
}