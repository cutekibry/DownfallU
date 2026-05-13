using DownfallU.DownfallUCode.Actions;
using DownfallU.DownfallUCode.Powers.Hermit;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class EternalForm : HermitCard
{
    public EternalForm() : base(3, CardType.Power, CardRarity.Rare, TargetType.Self)
    {
        WithPower<EternalPower>(4, false);
        WithKeyword(CardKeyword.Ethereal, UpgradeType.Remove);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await ShortActions.CardPower<EternalPower>(this, play, ctx);
    }

}
