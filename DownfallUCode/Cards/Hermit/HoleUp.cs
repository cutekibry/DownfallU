
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using BaseLib.Utils;
using DownfallU.DownfallUCode.Actions;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class HoleUp : HermitCard
{
    public HoleUp() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
        WithBlock(12, 4);
        WithPower<WeakPower>(2, true);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardBlock(this, play);
        await ShortActions.CardPower<WeakPower>(this, play, ctx);
    }

}
