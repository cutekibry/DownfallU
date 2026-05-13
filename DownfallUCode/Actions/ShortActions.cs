using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace DownfallU.DownfallUCode.Actions;

public static class ShortActions
{
    private static async Task ApplyPower<T>(CardModel card, Creature target, PlayerChoiceContext ctx) where T : PowerModel
    {
        await PowerCmd.Apply<T>(ctx, target, card.DynamicVars[typeof(T).Name].BaseValue, card.Owner.Creature, card);
    }
    public static async Task CardPower<T>(CardModel card, CardPlay play, PlayerChoiceContext ctx) where T : PowerModel
    {
        switch (card.TargetType)
        {
            case TargetType.Self:
                await ApplyPower<T>(card, card.Owner.Creature, ctx);
                break;
            case TargetType.AnyEnemy:
                await ApplyPower<T>(card, play.Target!, ctx);
                break;
            case TargetType.AllEnemies:
                foreach (var enemy in card.CombatState!.HittableEnemies)
                {
                    await ApplyPower<T>(card, enemy, ctx);
                }
                break;
            case TargetType.RandomEnemy:
                var randomEnemy = card.Owner.RunState.Rng.CombatTargets.NextItem(card.CombatState!.HittableEnemies);
                if (randomEnemy != null)
                {
                    await ApplyPower<T>(card, randomEnemy, ctx);
                }
                break;
            default:
                throw new NotImplementedException($"TargetType {card.TargetType} not implemented for CardPower action.");
        }
    }
}