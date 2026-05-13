using BaseLib.Utils;
using DownfallU.DownfallUCode.Actions;
using DownfallU.DownfallUCode.Powers.Hermit;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class NoHoldsBarred : HermitCard
{
    public NoHoldsBarred() : base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies)
    {
        WithDamage(19, 4);
        WithPower<BruisePower>(5, 1, true);
        WithEnergy(1);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(ctx);
        await ShortActions.CardPower<BruisePower>(this, play, ctx);
        await PowerCmd.Apply<DrainedPower>(ctx, Owner.Creature, DynamicVars.Energy.BaseValue, Owner.Creature, this);
    }

}
