
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Runs;

namespace DownfallU.DownfallUCode.Utils;

public class UpgradedCardReward : CardReward
{
    public UpgradedCardReward(CardCreationOptions options, int cardCount, Player player)
        : base(options, cardCount, player)
    {
        AfterGenerated += UpgradeGeneratedCards;
    }

    public UpgradedCardReward(IEnumerable<CardModel> cardsToOffer, CardCreationSource source, Player player, CardCreationOptions rerollOptions)
        : base(cardsToOffer, source, player, rerollOptions)
    {
        AfterGenerated += UpgradeGeneratedCards;
    }

    private void UpgradeGeneratedCards()
    {
        CardCmd.Upgrade(Cards, CardPreviewStyle.None);
    }
}