using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using DownfallU.DownfallUCode.Actions;
using MegaCrit.Sts2.Core.Models.Powers;

namespace DownfallU.DownfallUCode.Cards.Snecko;

public class SneckoForm : SneckoCard
{
    public SneckoForm() : base(3, CardType.Power, CardRarity.Ancient, TargetType.Self)
    {
        WithPower<MachineLearningPower>(2, false);
        WithPower<ConfusedPower>(1, true);
        WithKeyword(CardKeyword.Innate, UpgradeType.Add);
    }

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await ShortActions.CardPower<MachineLearningPower>(this, play, ctx);
        await ShortActions.CardPower<ConfusedPower>(this, play, ctx);
    }
}
