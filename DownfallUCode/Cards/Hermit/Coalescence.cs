
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using BaseLib.Utils;
using DownfallU.DownfallUCode.Powers.Hermit;
using DownfallU.DownfallUCode.Actions;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class Coalescence : HermitCard
{
    public Coalescence() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
        WithBlock(6, 3);
        WithPower<CoalescencePower>(2, 1, false);
        WithTip(CardKeyword.Retain);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardBlock(this, play);
        await ShortActions.CardPower<CoalescencePower>(this, play, ctx);
    }
}
