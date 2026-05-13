using BaseLib.Utils;
using DownfallU.DownfallUCode.Character.Hermit;
using DownfallU.DownfallUCode.Utils.Hermit;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class Ricochet : HermitCard
{
    
    public Ricochet() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
        WithDamage(7, 2);
        WithCalculatedVar("Hits", 0, CountDeadOnEffects);
        WithTip(HermitKeywords.DeadOn);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        var extraHits = GetCalculatedValue("Hits", play);
        await CommonActions.CardAttack(this, play).Execute(ctx);
        for (int i = 0; i < extraHits; i++)
        {
            var enemies = CombatState?.HittableEnemies.ToList();
            if (enemies == null || enemies.Count == 0) break;
    
            await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
                .FromCard(this)
                .TargetingRandomOpponents(CombatState!)
                .Execute(ctx);
        }
    }

    private static decimal CountDeadOnEffects(CardModel card, Creature? _)
    {
        return DeadOnCounter.GetCounter(card.Owner);
    }

}
