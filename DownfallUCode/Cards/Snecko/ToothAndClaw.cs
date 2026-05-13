using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using BaseLib.Utils;
using DownfallU.DownfallUCode.Extensions;

namespace DownfallU.DownfallUCode.Cards.Snecko;

// Reference: STS1 ToothAndClaw; local GiftFilter handling via SneckoSoul.
public class ToothAndClaw : SneckoCard
{
    public override Func<CardModel, bool>? GiftFilter => c => c.Rarity == CardRarity.Uncommon;

    public ToothAndClaw() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
        WithDamage(4, 2);
        WithCalculatedVar("Colors", 0, CountUniquePoolsInHand);
        WithUpgradingCardTip<Shiv>();
    }

    private static decimal CountUniquePoolsInHand(CardModel card, MegaCrit.Sts2.Core.Entities.Creatures.Creature? _)
    {
        return card.Owner.GetHand()
            .Where(c => c != card)
            .Select(c => c.Pool.ColorId())
            .Distinct()
            .Count();
    }

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(ctx);

        var amount = GetCalculatedValue("Colors", play);

        for (var i = 0; i < amount; i++)
        {
            var card = CombatState!.CreateCard<Shiv>(Owner);
            if (IsUpgraded)
                CardCmd.Upgrade(card);
            await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, Owner);
        }
    }
}
