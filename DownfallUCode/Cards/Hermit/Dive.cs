
using BaseLib.Utils;
using DownfallU.DownfallUCode.Actions;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class Dive : HermitCard
{
    public override bool HasDeadOn => true;

    public Dive() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
        WithBlock(8, 2);
        WithPower<PlatingPower>(1, 1, true);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardBlock(this, play);
    }
    protected override async Task AfterPlayInternalIfDeadOn(PlayerChoiceContext ctx, CardPlay play)
    {
        await ShortActions.CardPower<PlatingPower>(this, play, ctx);
    }

}
