using DownfallU.DownfallUCode.Powers.Hermit;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using DownfallU.DownfallUCode.Character.Hermit;
using DownfallU.DownfallUCode.Actions;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class HighNoon : HermitCard
{
    public HighNoon() : base(1, CardType.Power, CardRarity.Rare, TargetType.Self)
    {
        WithTip(HermitKeywords.Strike);
        WithTip(HermitKeywords.Defend);
        WithCostUpgradeBy(-1);
        WithPower<HighNoonPower>(1);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await ShortActions.CardPower<HighNoonPower>(this, play, ctx);
    }
}
