using DownfallU.DownfallUCode.Actions;
using DownfallU.DownfallUCode.Powers.Hermit;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class FatalDesire : HermitCard
{
    public FatalDesire() : base(1, CardType.Power, CardRarity.Rare, TargetType.Self)
    {
        WithTip(typeof(Injury));
        WithPower<FatalDesirePower>(1, false);
        WithPower<MachineLearningPower>(2, false);
        WithKeyword(CardKeyword.Innate, UpgradeType.Add);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await ShortActions.CardPower<FatalDesirePower>(this, play, ctx);
        await ShortActions.CardPower<MachineLearningPower>(this, play, ctx);
    }

}
