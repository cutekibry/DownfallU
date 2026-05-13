using DownfallU.DownfallUCode.Actions;
using DownfallU.DownfallUCode.Powers.Hermit;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class Snipe : HermitCard
{
    public Snipe() : base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithKeyword(CardKeyword.Exhaust);
        WithPower<SnipePower>(1, false);
        WithPower<ConcentrationPower>(0, 1, false);
        WithTips(card => card.IsUpgraded ? [HoverTipFactory.FromPower<ConcentrationPower>()] : []);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await ShortActions.CardPower<SnipePower>(this, play, ctx);
        await ShortActions.CardPower<ConcentrationPower>(this, play, ctx);
    }

}
