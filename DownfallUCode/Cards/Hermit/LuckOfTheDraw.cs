
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class LuckOfTheDraw : HermitCard
{
    public LuckOfTheDraw() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithEnergy(3, 1);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        var threshold = DynamicVars.Energy.IntValue;
        int totalCost = 0;

        while (totalCost < threshold && PileType.Hand.GetPile(Owner)!.Cards.Count < 10)
        {
            var cards = (await CardPileCmd.Draw(ctx, 1, Owner)).ToList();
            if(cards.Count == 0)
                break;

            var card = cards[0];
            if (card.EnergyCost.CostsX)
                totalCost += Owner.PlayerCombatState!.Energy;
            else
                totalCost += Math.Max(0, card.EnergyCost.GetResolved());
        }
    }

}
