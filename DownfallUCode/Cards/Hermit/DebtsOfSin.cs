using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class DebtsOfSin : HermitCard
{
    private static readonly System.Reflection.MethodInfo CreateCardGenericMethod =
        typeof(CombatState).GetMethods()
            .Single(m => m.Name == nameof(CombatState.CreateCard)
                && m.IsGenericMethodDefinition
                && m.GetParameters().Length == 1);

    public DebtsOfSin() : base(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
    {
        WithKeyword(CardKeyword.Exhaust);
        WithTip(CardKeyword.Unplayable);
        WithKeyword(CardKeyword.Retain, UpgradeType.Add);
    }

    public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        

        var players = CombatState!.GetTeammatesOf(Owner.Creature)
            .Where(c => c is { IsAlive: true, IsPlayer: true, Player: not null } && c.Player != Owner);

        foreach (Creature creature in players)
        {
            foreach (var pileType in new[] { PileType.Hand, PileType.Discard, PileType.Draw }) 
            {
                var unplayableCards = pileType.GetPile(creature.Player!).Cards
                    .Where(c => c.Keywords.Contains(CardKeyword.Unplayable))
                    .ToList();

                foreach (var card in unplayableCards)
                {
                    await CardCmd.Exhaust(ctx, card);
                    if (card.Type == CardType.Curse)
                    {
                        var newCard = CreateCardOfSameType(card);
                        await CardPileCmd.AddGeneratedCardToCombat(newCard, PileType.Hand, Owner);
                    }
                }
            }
        }
    }

    private CardModel CreateCardOfSameType(CardModel card)
    {
        return (CardModel)CreateCardGenericMethod
            .MakeGenericMethod(card.GetType())
            .Invoke(CombatState, [Owner])!;
    }

}
