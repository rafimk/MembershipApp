using Membership.Infrastructure.OCR.Consts;

namespace Membership.Infrastructure.OCR.Policies;

public interface ICardReadPolicy
{
    bool CanBeApplied(CardSide cardSide);
    OcrData ReadData(CardSide cardSide, string result);
}