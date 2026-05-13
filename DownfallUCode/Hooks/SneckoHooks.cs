using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using DownfallU.DownfallUCode.Cards.Snecko;
using DownfallU.DownfallUCode.Powers.Snecko;
using DownfallU.DownfallUCode.Relics.Snecko;

namespace DownfallU.DownfallUCode.Hooks;

public static class SneckoHooks
{
    public static async Task TriggerAfterCardMuddled(PlayerChoiceContext ctx, CardModel card)
    {
        if (card is SneckoCard sneckoCard)
            await sneckoCard.AfterMuddled(ctx);

        var powers = card.Owner.Creature.Powers.Where(p => p is SneckoPower);
        foreach (var power in powers)
            await ((SneckoPower)power).AfterCardMuddled(ctx, card);

        var relics = card.Owner.Relics.Where(r => r is SneckoRelic);
        foreach (var relic in relics)
            await ((SneckoRelic)relic).AfterCardMuddled(ctx, card);
    }
}