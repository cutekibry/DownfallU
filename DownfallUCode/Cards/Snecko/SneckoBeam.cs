using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using DownfallU.DownfallUCode.Actions;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Commands;
using BaseLib.Utils;

namespace DownfallU.DownfallUCode.Cards.Snecko;

public class SneckoBeam : SneckoCard
{
    public override bool HasOverflow => true;
    public SneckoBeam() : base(2, CardType.Attack, CardRarity.Ancient, TargetType.AllEnemies)
    {
        WithDamage(10);
        WithPower<VulnerablePower>(1, 1, true);
        WithPower<WeakPower>(1, 1, true);
        WithStrengthLoss(1, 1);
    }

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(ctx);
        await ShortActions.CardPower<VulnerablePower>(this, play, ctx);
        await ShortActions.CardPower<WeakPower>(this, play, ctx);
        if (IsOverflowed)
        {
            foreach (var enemy in CombatState!.HittableEnemies)
            {
                await PowerCmd.Apply<StrengthPower>(ctx, enemy, -DynamicVars["StrengthLoss"].BaseValue, Owner.Creature, this);
            }
        }
    }
}
