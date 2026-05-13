using MegaCrit.Sts2.Core.Localization;

namespace DownfallU.DownfallUCode.CardSelectorPref;

public readonly struct DownfallUCardSelectorPrefs
{
    public static LocString MuddleSelectionPrompt => new("card_selection", "TO_MUDDLE"); public LocString Prompt { get; }
}