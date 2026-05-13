
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using BaseLib.Utils;
using DownfallU.DownfallUCode.Actions;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class Dissolve : HermitCard
{

    public Dissolve() : base(2, CardType.Skill, CardRarity.Rare, TargetType.Self)
    {
        WithBlock(18, 7);
        WithPower<BlurPower>(2, false);
        WithKeyword(CardKeyword.Exhaust);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardBlock(this, play);
        await ShortActions.CardPower<BlurPower>(this, play, ctx);
    }

}
