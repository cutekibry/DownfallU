using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace DownfallU.DownfallUCode.Character.Hermit;

public static class HermitKeywords
{
    [CustomEnum]
    [KeywordProperties(AutoKeywordPosition.None)]
    public static CardKeyword DeadOn;

    [CustomEnum]
    [KeywordProperties(AutoKeywordPosition.None)]
    public static CardKeyword Bounty;

    [CustomEnum]
    [KeywordProperties(AutoKeywordPosition.None)]
    public static CardKeyword Strike;

    [CustomEnum]
    [KeywordProperties(AutoKeywordPosition.None)]
    public static CardKeyword Defend;
}
