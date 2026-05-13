
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Extensions;

namespace DownfallU.DownfallUCode.Extensions;

public static class PlayerExtensions
{
    public static IReadOnlyList<CardModel> GetHand(this Player player)
    {
        return PileType.Hand.GetPile(player).Cards;
    }
    public static IReadOnlyList<CardModel> GetDiscard(this Player player)
    {
        return PileType.Discard.GetPile(player).Cards;
    }
    public static IReadOnlyList<CardModel> GetDraw(this Player player)
    {
        return PileType.Draw.GetPile(player).Cards;
    }
    public static IReadOnlyList<CardModel> GetDeck(this Player player)
    {
        return PileType.Deck.GetPile(player).Cards;
    }
    public static IEnumerable<T> CombatRandomTake<T>(this List<T> list, int amount, Player player)
     where T : IComparable<T>
    {
        return list.StableShuffle(player.RunState.Rng.CombatCardSelection).Take(amount);
    }
    public static IEnumerable<T> CombatRandomTake<T>(this IEnumerable<T> list, int amount, Player player)
     where T : IComparable<T>
    {
        return list.ToList().StableShuffle(player.RunState.Rng.CombatCardSelection).Take(amount);
    }
}