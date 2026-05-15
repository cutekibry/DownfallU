
using System.ComponentModel.DataAnnotations;
using DownfallU.DownfallUCode.Character.Snecko;
using DownfallU.DownfallUCode.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Runs;

namespace DownfallU.DownfallUCode.Relics.Snecko;

public class UnknownEgg : SneckoCoreRelic
{
    public override bool HasUponPickupEffect => true;
    public UnknownEgg() : base(RelicRarity.Rare)
    {
        WithTip(SneckoKeyword.Offclass);
        WithCards(2);
    }
    public override Task AfterObtained()
    {
        var enumerable = Owner.GetDeck().Where(c => c != null && c.IsOffclass() && c.IsUpgradable).ToList().StableShuffle(Owner.RunState.Rng.Niche).Take(DynamicVars.Cards.IntValue);
        foreach (var item in enumerable)
        {
            CardCmd.Upgrade(item);
        }
        return Task.CompletedTask;
    }
    public override bool TryModifyCardRewardOptionsLate(Player player, List<CardCreationResult> cardRewards, CardCreationOptions options)
    {
        if (player != Owner || options.Flags.HasFlag(CardCreationFlags.NoHookUpgrades))
        {
            return false;
        }

        UpgradeValidCards(cardRewards, c => c.IsOffclass(), this);
        return true;
    }

    public override void ModifyMerchantCardCreationResults(Player player, List<CardCreationResult> cards)
    {
        if (player == Owner)
        {
            UpgradeValidCards(cards, c => c.IsOffclass(), this);
        }
    }

    public override bool TryModifyCardBeingAddedToDeck(CardModel card, out CardModel? newCard)
    {
        newCard = null;
        if (card.Owner != Owner || !card.IsOffclass() || !card.IsUpgradable)
        {
            return false;
        }

        newCard = Owner.RunState.CloneCard(card);
        CardCmd.Upgrade(newCard, CardPreviewStyle.None);
        return true;
    }


    private static void UpgradeValidCards(List<CardCreationResult> cards, Func<CardModel, bool> filter, RelicModel eggRelic)
    {
        foreach (CardCreationResult card3 in cards)
        {
            CardModel card = card3.Card;
            if (filter(card) && card.IsUpgradable)
            {
                CardModel card2 = eggRelic.Owner.RunState.CloneCard(card);
                CardCmd.Upgrade(card2);
                card3.ModifyCard(card2, eggRelic);
            }
        }
    }
}