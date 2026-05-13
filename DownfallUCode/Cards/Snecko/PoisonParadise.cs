using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using DownfallU.DownfallUCode.Character.Snecko;
using DownfallU.DownfallUCode.Powers.Snecko;

namespace DownfallU.DownfallUCode.Cards.Snecko;

// Reference: STS1 PoisonParadise.
public class PoisonParadise : SneckoCard
{
    public PoisonParadise() : base(1, CardType.Power, CardRarity.Rare, TargetType.Self)
    {
        WithPower<FountainPower>(4, 2, false);
        WithTip(typeof(VenomPower));
        WithKeyword(SneckoKeyword.Overflow);
    }

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await PowerCmd.Apply<FountainPower>(ctx, Owner.Creature, DynamicVars["FountainPower"].BaseValue, Owner.Creature, this);
    }
}
