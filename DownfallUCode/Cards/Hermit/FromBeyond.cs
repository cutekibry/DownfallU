using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class FromBeyond : HermitCard
{
    public FromBeyond() : base(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
    {
        WithHpLoss(5, 2);
        WithCalculatedVar("Hits", 0, CountCardsInExhaust);
        WithTip(CardKeyword.Exhaust);
    }

    private static decimal CountCardsInExhaust(CardModel card, Creature? _)
    {
        return PileType.Exhaust.GetPile(card.Owner).Cards.Count;
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        var hits = (int)GetCalculatedValue("Hits", play);
        for (int i = 0; i < hits; i++)
        {
            Creature? enemy = Owner.RunState.Rng.CombatTargets.NextItem(CombatState!.HittableEnemies);
            if (enemy == null)
            {
                break;
            }

            await CreatureCmd.Damage(ctx, enemy, DynamicVars["HpLoss"].BaseValue, ValueProp.Unblockable | ValueProp.Unpowered, Owner.Creature, this);
        }
    }

}
