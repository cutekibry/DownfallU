using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using DownfallU.DownfallUCode.Character.Snecko;
using DownfallU.DownfallUCode.Actions;

namespace DownfallU.DownfallUCode.Cards.Snecko;

// Reference: STS1 PureSnecko.
public class PureSnecko : SneckoCard
{
    public PureSnecko() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithCards(3, 1);
        WithKeyword(SneckoKeyword.Muddle);
    }

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        var cards = await CardPileCmd.Draw(ctx, DynamicVars.Cards.IntValue, Owner);
        await SneckoActions.Muddle(ctx, cards);
    }
}
