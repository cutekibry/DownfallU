using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class Grudge : HermitCard
{
    public Grudge() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies)
    {
        WithCalculatedDamage(9, 2, CountCurses, 0, 1);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(ctx);
    }

    private static decimal CountCurses(CardModel card, Creature? _)
    {
        int curseCount = 0;
        foreach (var pileType in new[] { PileType.Draw, PileType.Hand, PileType.Discard })
        {
            curseCount += pileType.GetPile(card.Owner).Cards.Count(c => c.Type == CardType.Curse);
        }

        return curseCount;
    }

}
