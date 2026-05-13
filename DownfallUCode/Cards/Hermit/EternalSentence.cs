using DownfallU.DownfallUCode.Actions;
using DownfallU.DownfallUCode.Powers.Hermit;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class EternalSentence : HermitCard
{

    public EternalSentence() : base(3, CardType.Power, CardRarity.Ancient, TargetType.Self)
    {
        WithPower<EternalSentencePower>(1);
        WithKeyword(CardKeyword.Ethereal, UpgradeType.Remove);
    }
    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await ShortActions.CardPower<EternalSentencePower>(this, play, ctx);
    }

}
