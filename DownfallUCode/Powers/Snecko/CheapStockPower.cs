using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using DownfallU.DownfallUCode.Actions;
using DownfallU.DownfallUCode.Extensions;

namespace DownfallU.DownfallUCode.Powers.Snecko;


public class CheapStockPower : SneckoPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext ctx, Player player)
    {
        if (player != Owner.Player)
            return;
        
        var candidateCards = PileType.Hand.GetPile(Owner.Player).Cards.Where(c => c.CanBeMuddled());
        var sortedCandidateCards = candidateCards.ToList().StableShuffle(Owner.Player.RunState.Rng.Shuffle).OrderBy(c => -c.EnergyCost.GetResolved());

        var cardsToMuddle = sortedCandidateCards.Take(Amount);
        await SneckoActions.Muddle(ctx, cardsToMuddle);
    }
}