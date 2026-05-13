using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using DownfallU.DownfallUCode.Character.Snecko;
using DownfallU.DownfallUCode.Extensions;

namespace DownfallU.DownfallUCode.Cards.Snecko;

public class ComboString : SneckoCard
{
    public override Func<CardModel, bool>? GiftFilter => c => c.Rarity == CardRarity.Uncommon;  
    public ComboString() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
        WithDamage(7, 2);
        WithCalculatedVar("Hits", 0, GetHits);
        WithKeyword(SneckoKeyword.Offclass);
    }
    private static decimal GetHits(CardModel card, Creature? _)
    {
        return CombatManager.Instance.History.Entries.OfType<CardPlayFinishedEntry>().Count(e => e.CardPlay.Card.IsOffclass() && e.Actor == card.Owner.Creature && e.HappenedThisTurn(card.CombatState));
    }

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardAttack(this, play, (int)GetCalculatedValue("Hits", play)).Execute(ctx);
    }
}