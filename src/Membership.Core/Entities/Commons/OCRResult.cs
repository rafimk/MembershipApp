using Membership.Core.Consts;

namespace Membership.Core.Entities.Commons;

public class OcrResult
{
    public Guid Id { get; private set; }
    public Guid? FrontPageId { get; private set; }
    public Guid? LastPageId { get; private set; }
    public string IdNumber { get; set; }
    public string Name { get; set;}
    public DateOnly DateofBirth { get; set;}
    public DateOnly ExpiryDate { get; set; }
    public string CardNumber { get; set;}
    public CardType CardType { get; set;}
    public DateTime CreatedAt { get; private set; }
    public Guid CreatedBy { get; private set; }

    private OcrResult(Guid id, Guid? frontPageId, Guid? lastPageId, string idNumber, string name, 
        DateOnly dateofBirth, DateOnly expiryDate, string cardNumber, CardType cardType, DateTime createdAt, Guid createdBy)
    {
        Id = id;
        FrontPageId = frontPageId;
        LastPageId = lastPageId;
        IdNumber = idNumber;
        Name = name;
        DateofBirth= dateofBirth;
        ExpiryDate = expiryDate;
        CardNumber = cardNumber;
        CardType = cardType;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
    }

    private OcrResult()
    {
    }
    
    public static OcrResult Create(Guid id, Guid? frontPageId, Guid? lastPageId, string idNumber, string name, 
        DateOnly dateofBirth, DateOnly expiryDate, string cardNumber, CardType cardType, DateTime createdAt, Guid createdBy)
        => new(id, frontPageId, lastPageId, idNumber, name, dateofBirth, expiryDate, cardNumber, cardType, createdAt, createdBy);

    public void Update(Guid? frontPageId, Guid? lastPageId, string idNumber, string name, 
        DateOnly dateofBirth, DateOnly expiryDate, CardType cardType,string cardNumber)
    {
        FrontPageId = frontPageId;
        LastPageId = lastPageId;
        IdNumber = idNumber;
        Name = name;
        DateofBirth= dateofBirth;
        ExpiryDate = expiryDate;
        CardNumber = CardNumber;
        CardType = cardType;
    }
}
