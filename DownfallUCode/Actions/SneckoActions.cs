using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Cards;
using DownfallU.DownfallUCode.CardSelectorPref;
using DownfallU.DownfallUCode.Extensions;
using DownfallU.DownfallUCode.Hooks;
using DownfallU.DownfallUCode.Character.Snecko;

namespace DownfallU.DownfallUCode.Actions;

public static class SneckoActions
{
    public static IEnumerable<CardModel> GenerateRandomOffclassCards(Player player, int amount)
    {
        var candidates = player.UnlockState.CardPools.Where(p => p is not SneckoCardPool).SelectMany(p => p.AllCards).Where(c => c.CanBeGeneratedInCombat && c.Rarity != CardRarity.Basic && c.Rarity != CardRarity.Ancient);
        return CardFactory.GetDistinctForCombat(player, candidates, amount, player.RunState.Rng.CombatCardGeneration);
    }

    public static async Task Muddle(PlayerChoiceContext ctx, CardModel card, bool cheaperOnly = false)
    {
        if (card.EnergyCost.Canonical < 0 || card.EnergyCost.CostsX)
        {
            return;
        }

        var cost = card.Owner.RunState.Rng.CombatEnergyCosts.NextInt(
            cheaperOnly ? Math.Max(card.EnergyCost.GetResolved(), 1) : 4
        );
        card.EnergyCost.SetThisTurn(cost);
        NCard.FindOnTable(card)?.PlayRandomizeCostAnim();
        await SneckoHooks.TriggerAfterCardMuddled(ctx, card);
    }
    public static async Task MuddleHand(PlayerChoiceContext ctx, CardModel card, int amount, bool cheaperOnly = false)
    {
        var cards = await CardSelectCmd.FromHand(ctx, card.Owner, new CardSelectorPrefs(DownfallUCardSelectorPrefs.MuddleSelectionPrompt, amount), c => c.CanBeMuddled(), card);
        await Muddle(ctx, cards, cheaperOnly);
    }
    public static async Task MuddleHand(PlayerChoiceContext ctx, CardModel card, bool cheaperOnly = false)
    {
        await MuddleHand(ctx, card, card.DynamicVars["Muddle"].IntValue, cheaperOnly);
    }

    public static async Task Muddle(PlayerChoiceContext ctx, IEnumerable<CardModel> cards, bool cheaperOnly = false)
    {
        foreach (var card in cards)
            await Muddle(ctx, card, cheaperOnly);
    }
}