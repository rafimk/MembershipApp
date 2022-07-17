using Membership.Infrastructure.Exceptions;

namespace Membership.Infrastructure.OCR.Consts;

public sealed record CardSide
{

    public static IEnumerable<string> AvailableSides { get; } = new[] {"new-card-front-side", 
        "old-card-front-side", "new-card-back-side", "old-card-back-side"};

    public string Value { get; }

    public CardSide(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 30)
        {
            throw new InvalidCardSideException(value);
        }

        if (!AvailableSides.Contains(value))
        {
            throw new InvalidCardSideException(value);
        }

        Value = value;
    }

    public static CardSide NewCardFrontSide() => new("new-card-front-side");
    public static CardSide OldCardFrontSide() => new("old-card-front-side");
    public static CardSide NewCardBackSide() => new("new-card-back-side");
    public static CardSide OldCardBackSide() => new("old-card-back-side");

    public static implicit operator CardSide(string value) => new CardSide(value);

    public static implicit operator string(CardSide value) => value?.Value;

    public override string ToString() => Value;
}