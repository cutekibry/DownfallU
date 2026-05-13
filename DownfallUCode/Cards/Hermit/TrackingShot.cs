
using BaseLib.Utils;
using DownfallU.DownfallUCode.Powers.Hermit;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class TrackingShot : HermitCard
{
    public TrackingShot() : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
    {
        WithDamage(5, 2);
        WithRepeat(2);
        WithPower<ConcentrationPower>(1, true);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await PowerCmd.Apply<ConcentrationPower>(ctx, Owner.Creature, DynamicVars["ConcentrationPower"].BaseValue, Owner.Creature, this);
        await CommonActions.CardAttack(this, play, DynamicVars.Repeat.IntValue).Execute(ctx);
    }

}
