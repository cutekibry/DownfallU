using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using DownfallU.DownfallUCode.Actions;
using DownfallU.DownfallUCode.Extensions;
using DownfallU.DownfallUCode.Character.Snecko;

namespace DownfallU.DownfallUCode.Cards.Snecko;

// Reference: STS1 Restock; local FlashInThePan for discarding hand and PureSnecko for draw-then-Muddle.
public class Restock : SneckoCard
{
    public Restock() : base(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
    {
        WithCards(6);
        WithKeyword(CardKeyword.Exhaust);
        WithKeyword(SneckoKeyword.Muddle);
        WithCostUpgradeBy(-1);
    }

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CardCmd.Discard(ctx, Owner.GetHand());
        await CardPileCmd.Draw(ctx, DynamicVars.Cards.IntValue, Owner);
        await SneckoActions.Muddle(ctx, Owner.GetHand().Where(c => c.CanBeMuddled()));
    }
}
