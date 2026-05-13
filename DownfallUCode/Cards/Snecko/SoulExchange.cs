using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using DownfallU.DownfallUCode.Actions;
using DownfallU.DownfallUCode.Extensions;
using DownfallU.DownfallUCode.Character.Snecko;

namespace DownfallU.DownfallUCode.Cards.Snecko;

// Reference: STS1 SoulExchange; local PureSnecko/Restock for Muddle whole hand.
public class SoulExchange : SneckoCard
{
    public SoulExchange() : base(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
    {
        WithKeywords(CardKeyword.Retain, CardKeyword.Exhaust);
        WithKeyword(SneckoKeyword.Muddle);
        WithCostUpgradeBy(-1);
    }

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await SneckoActions.Muddle(ctx, PileType.Hand.GetPile(Owner).Cards.Where(c => c.CanBeMuddled()));
    }
}
