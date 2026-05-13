
using DownfallU.DownfallUCode.Actions;
using DownfallU.DownfallUCode.Powers.Hermit;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class Brawl : HermitCard
{
    public Brawl() : base(2, CardType.Power, CardRarity.Uncommon, TargetType.Self)
    {
        WithPower<BrawlPower>(3, 2, false);
        WithTip(typeof(BruisePower));
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await ShortActions.CardPower<BrawlPower>(this, play, ctx);
    }
}
