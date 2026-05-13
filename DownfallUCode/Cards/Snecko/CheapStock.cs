using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using DownfallU.DownfallUCode.Character.Snecko;
using DownfallU.DownfallUCode.Powers.Snecko;

namespace DownfallU.DownfallUCode.Cards.Snecko;


public class CheapStock : SneckoCard
{
    public CheapStock() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self)
    {
        WithPower<CheapStockPower>(1, 1, false);
        WithKeyword(SneckoKeyword.Muddle);
    }

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await PowerCmd.Apply<CheapStockPower>(ctx, Owner.Creature, DynamicVars["CheapStockPower"].BaseValue, Owner.Creature, this);
    }
}