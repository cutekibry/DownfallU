using BaseLib.Utils;
using DownfallU.DownfallUCode.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace DownfallU.DownfallUCode.Cards.Snecko;

// Reference: STS1 FlashInThePan; STS2 Predator for DrawCardsNextTurnPower and Hermit Roulette for discarding hand.
public class FlashInThePan : SneckoCard
{
    public FlashInThePan() : base(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithBlock(13, 3);
    }

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardBlock(this, play);
        var handCards = Owner.GetHand().ToList();
        await CardCmd.Discard(ctx, handCards);
        if (handCards.Count > 0)
            await PowerCmd.Apply<DrawCardsNextTurnPower>(ctx, Owner.Creature, handCards.Count, Owner.Creature, this);
    }
}
