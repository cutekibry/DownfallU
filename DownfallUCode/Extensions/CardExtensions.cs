
using MegaCrit.Sts2.Core.Models;
using DownfallU.DownfallUCode.Character.Snecko;

namespace DownfallU.DownfallUCode.Extensions;

public static class CardExtensions
{
    public static bool CanBeMuddled<TCard>(this TCard card) where TCard : CardModel
    {
        return !card.EnergyCost.CostsX && card.EnergyCost.Canonical >= 0;
    }
    public static bool IsOffclass<TCard>(this TCard card) where TCard : CardModel
    {
        return card.Pool is not SneckoCardPool;
    }
}