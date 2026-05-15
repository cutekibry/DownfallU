
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models.CardPools;

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
    public static IReadOnlyList<CardModel> GetExhaust(this Player player)
    {
        return PileType.Exhaust.GetPile(player).Cards;
    }

    public static IReadOnlyList<CardPoolModel> GetOffclassCardPoolsForGift(this Player player)
    {
        return [..player.UnlockState.CharacterCardPools.Where(p => p.IsOffclass()), ..player.UnlockState.CardPools.Where(p => p is ColorlessCardPool)];
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
    public static Creature? RandomEnemy(this Player player)
    {
        return player.RunState.Rng.CombatTargets.NextItem(player.Creature.CombatState!.HittableEnemies);
    }
}