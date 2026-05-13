using DownfallU.DownfallUCode.Actions;
using DownfallU.DownfallUCode.Powers.Hermit;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class TakeAim : HermitCard
{
    public TakeAim() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self)
    {
        WithPower<ConcentrationPower>(1, true);
        WithCostUpgradeBy(-1);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await ShortActions.CardPower<ConcentrationPower>(this, play, ctx);
    }

}
