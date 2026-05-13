
using DownfallU.DownfallUCode.Actions;
using DownfallU.DownfallUCode.Powers.Hermit;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class Determination : HermitCard
{
    public Determination() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self)
    {
        WithTip(typeof(StrengthPower));
        WithPower<DeterminationPower>(1);
        WithKeyword(CardKeyword.Innate, UpgradeType.Add);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await ShortActions.CardPower<DeterminationPower>(this, play, ctx);
    }

}
