using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class DeadMansHand : HermitCard
{
    public DeadMansHand() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithCards(3);
        WithCostUpgradeBy(-1);
    }

    private static int RarityLevel(CardRarity rarity) => rarity switch
    {
        CardRarity.Ancient => 3,
        CardRarity.Rare => 2,
        CardRarity.Uncommon => 1,
        _ => 0,
    };

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        

        // Discard entire hand
        var handCards = PileType.Hand.GetPile(Owner).Cards.ToList();
        if (handCards.Count > 0)
        {
            await CardCmd.Discard(ctx, handCards);
        }

        var drawCards = PileType.Draw.GetPile(Owner).Cards.ToList();
        if (drawCards.Count > 0)
        {
            for (int i = 0; i < DynamicVars.Cards.IntValue && drawCards.Count > 0; i++)
            {
                int selectedIndex = 0;
                int selectedRarity = RarityLevel(drawCards[0].Rarity);
                for (int j = 1; j < drawCards.Count; j++)
                {
                    int rarity = RarityLevel(drawCards[j].Rarity);
                    if (rarity >= selectedRarity)
                    {
                        selectedIndex = j;
                        selectedRarity = rarity;
                    }
                }

                var cardToDraw = drawCards[selectedIndex];
                drawCards.RemoveAt(selectedIndex);
                await CardPileCmd.Add(cardToDraw, PileType.Hand);
            }
        }
    }

}
