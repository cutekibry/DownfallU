
using DownfallU.DownfallUCode.Actions;
using DownfallU.DownfallUCode.Powers.Hermit;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class Horror : HermitCard
{
    public Horror() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AllEnemies)
    {
        WithPower<BruisePower>(3, 2, true);
        WithPower<HorrorPower>(1, 0, false);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await ShortActions.CardPower<BruisePower>(this, play, ctx);
        await ShortActions.CardPower<HorrorPower>(this, play, ctx);
    }

}
