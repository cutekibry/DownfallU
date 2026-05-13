using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using DownfallU.DownfallUCode.Utils.Snecko;

namespace DownfallU.DownfallUCode.Cards.Snecko;

// Reference: STS1 LastStrike; local ComboString for combat-history hit calculation and GiftFilter traceability.
public class LastStrike : SneckoCard
{
    public override Func<CardModel, bool>? GiftFilter => c => c.Rarity != CardRarity.Basic && c.Tags.Contains(CardTag.Strike);

    public LastStrike() : base(1, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
    {
        WithTags(CardTag.Strike);
        WithDamage(6, 3);
        WithCalculatedVar("Hits", 1, GetHits);
    }

    private static decimal GetHits(CardModel card, Creature? _)
    {
        var uniqueStrikeIds = CombatManager.Instance.History.Entries
            .OfType<CardPlayFinishedEntry>()
            .Where(e => e.Actor == card.Owner.Creature && e.CardPlay.Card.Tags.Contains(CardTag.Strike))
            .Select(e => e.CardPlay.Card.Id.Entry)
            .Distinct()
            .Count();

        return uniqueStrikeIds;
    }

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardAttack(this, play, (int)GetCalculatedValue("Hits", play)).Execute(ctx);
    }
}
