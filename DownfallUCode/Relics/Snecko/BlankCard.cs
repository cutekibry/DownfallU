using BaseLib.Utils;
using DownfallU.DownfallUCode.Enchantments;
using DownfallU.DownfallUCode.Extensions;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.RelicPools;

namespace DownfallU.DownfallUCode.Relics.Snecko;

[Pool(typeof(SharedRelicPool))]
public class BlankCard : SneckoRelic
{
    public BlankCard() : base(RelicRarity.Uncommon)
    {
        WithCards(1);
        WithTip(typeof(Echo));
    }

    public override async Task BeforeHandDraw(Player player, PlayerChoiceContext ctx, ICombatState combatState)
    {
        if (player != Owner || combatState.RoundNumber != 1)
            return;

        var selects = player.GetDraw().Where(c => c.Type != CardType.Curse && c.Type != CardType.Status).CombatRandomTake(1, player).ToList();
        if (selects.Count == 0)
            return;
        
        var card = selects[0].CreateClone();
        CardCmd.ApplyKeyword(card, CardKeyword.Exhaust);
        CardCmd.ApplyKeyword(card, CardKeyword.Ethereal);
        card.SetToFreeThisTurn();
        await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, Owner);
    }
}