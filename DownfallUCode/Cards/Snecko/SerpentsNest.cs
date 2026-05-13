using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using DownfallU.DownfallUCode.Powers.Snecko;

namespace DownfallU.DownfallUCode.Cards.Snecko;

// Reference: STS1 SerpentsNest; STS2 StormPower for power-card trigger.
public class SerpentsNest : SneckoCard
{
    public override Func<CardModel, bool>? GiftFilter => c => c.Type == CardType.Power && c.Rarity == CardRarity.Uncommon;

    public SerpentsNest() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self)
    {
        WithPower<SerpentsNestPower>(7, 3, false);
    }

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await PowerCmd.Apply<SerpentsNestPower>(ctx, Owner.Creature, DynamicVars["SerpentsNestPower"].BaseValue, Owner.Creature, this);
    }
}
