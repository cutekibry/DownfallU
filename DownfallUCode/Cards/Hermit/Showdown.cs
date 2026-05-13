using BaseLib.Utils;
using DownfallU.DownfallUCode.Character.Hermit;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class Showdown : HermitCard
{
    public Showdown() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
        WithDamage(9, 3);
        WithTip(HermitKeywords.Strike);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(ctx);
        var strikes = PileType.Hand.GetPile(Owner).Cards
            .Where(c => c.Tags.Contains(CardTag.Strike))
            .ToList();

        foreach (var strike in strikes)
        {
            Creature? enemy = Owner.RunState.Rng.CombatTargets.NextItem(CombatState!.HittableEnemies);
            if (enemy == null)
                break;
            await CardCmd.AutoPlay(ctx, strike, enemy);
        }
    }

}
