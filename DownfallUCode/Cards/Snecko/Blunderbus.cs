using BaseLib.Utils;
using DownfallU.DownfallUCode.Extensions;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace DownfallU.DownfallUCode.Cards.Snecko;

public class Blunderbus : SneckoCard
{
    public Blunderbus() : base(1, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies)
    {
        WithDamage(8, 3);
        WithEnergy(3);
        WithCalculatedVar("Hits", 1, CalculateBonusHits);
    }

    private static decimal CalculateBonusHits(CardModel card, Creature? _)
    {
        return card.Owner.GetHand().Count(c => c.EnergyCost.GetResolved() >= card.DynamicVars.Energy.IntValue);
    }

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardAttack(this, play, (int)GetCalculatedValue("Hits", play)).Execute(ctx);
    }
}