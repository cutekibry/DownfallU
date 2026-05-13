
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Random;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class Gambit : HermitCard
{
    public Gambit() : base(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
    {
        WithCards(2, 1);
        WithKeyword(CardKeyword.Exhaust);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        for (int i = 0; i < DynamicVars.Cards.BaseValue; i++)
        {
            var discardedAttacks = PileType.Discard.GetPile(Owner).Cards.Where(c => c.Type == CardType.Attack);
            Rng combatCardSelection = Owner.RunState.Rng.CombatCardSelection;
            CardModel? card = combatCardSelection.NextItem(discardedAttacks);
            if (card == null) break;
            
            // Move from discard to hand and reduce cost by 1 this turn
            await CardPileCmd.Add(card, PileType.Hand);
            card.EnergyCost.AddThisTurnOrUntilPlayed(-1, reduceOnly: true);
        }
    }

}
