using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using BaseLib.Utils;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class Roughhouse : HermitCard
{
    public override bool HasDeadOn => true;

    public Roughhouse() : base(3, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
    {
        WithDamage(24, 6);
        WithBlock(20, 4);
    }

    protected override async Task BeforePlayInternalIfDeadOn(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardBlock(this, play);
    }
    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(ctx);
    }

}
