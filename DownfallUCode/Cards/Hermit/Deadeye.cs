using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using BaseLib.Utils;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class Deadeye : HermitCard
{
    public override bool HasDeadOn => true;
    public Deadeye() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
        WithDamage(5, 1);
        WithPower<StrengthPower>(2, 1);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(ctx);
    }
    protected override async Task AfterPlayInternalIfDeadOn(PlayerChoiceContext ctx, CardPlay play)
    {
        await PowerCmd.Apply<StrengthPower>(ctx, Owner.Creature, DynamicVars["StrengthPower"].IntValue, Owner.Creature, this);
    }
}
