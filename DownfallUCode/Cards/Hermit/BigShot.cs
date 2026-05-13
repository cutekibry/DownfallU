using DownfallU.DownfallUCode.Powers.Hermit;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using DownfallU.DownfallUCode.Character.Hermit;
using DownfallU.DownfallUCode.Actions;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class BigShot : HermitCard
{
    public BigShot() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self)
    {
        WithPower<BigShotPower>(3, 1, false);
        WithTip(typeof(VigorPower));
        WithKeyword(HermitKeywords.DeadOn);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await ShortActions.CardPower<BigShotPower>(this, play, ctx);
    }
}
