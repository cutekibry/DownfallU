
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class FinalCanter : HermitCard
{

    public FinalCanter() : base(0, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
    {
        WithDamage(10, 3);
        WithCalculatedVar("Hits", 0, CountCursesInHand);
        WithKeywords(CardKeyword.Retain, CardKeyword.Exhaust);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardAttack(this, play, (int)GetCalculatedValue("Hits", play)).Execute(ctx);
    }

    private static decimal CountCursesInHand(CardModel card, Creature? _)
    {
        return PileType.Hand.GetPile(card.Owner).Cards.Count(c => c.Type == CardType.Curse);
    }

}
