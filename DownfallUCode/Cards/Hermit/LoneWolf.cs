
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.CardSelection;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class LoneWolf : HermitCard
{
    public LoneWolf() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithCostUpgradeBy(-1);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        var handPile = PileType.Hand.GetPile(Owner);
        if (handPile == null || handPile.Cards.Count == 0) return;

        // Let player choose a card from hand
        var chosen = await CardSelectCmd.FromHand(ctx, Owner, new CardSelectorPrefs(CardSelectorPrefs.DiscardSelectionPrompt, 1), null, this);
        if (chosen != null && chosen.Any())
        {
            var chosenCard = chosen.First();
            chosenCard.EnergyCost.SetUntilPlayed(0, false);
            await CardCmd.Discard(ctx, handPile.Cards.Where(c => c != chosenCard));
        }
    }

}
