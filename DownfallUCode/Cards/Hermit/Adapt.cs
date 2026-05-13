using DownfallU.DownfallUCode.Powers.Hermit;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class Adapt : HermitCard
{
    public Adapt() : base(3, CardType.Power, CardRarity.Rare, TargetType.Self)
    {
        WithPower<AdaptPower>(8, false);
        WithTip(StaticHoverTip.Block);
        WithTip(CardKeyword.Exhaust);
        WithCostUpgradeBy(-1);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await PowerCmd.Apply<AdaptPower>(ctx, Owner.Creature, 1, Owner.Creature, this);
    }
}
