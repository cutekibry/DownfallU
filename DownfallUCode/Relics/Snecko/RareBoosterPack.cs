using DownfallU.DownfallUCode.Character.Snecko;
using DownfallU.DownfallUCode.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Runs;

namespace DownfallU.DownfallUCode.Relics.Snecko;

public class RareBoosterPack : SneckoRelic
{
    public override bool HasUponPickupEffect => true;

    public RareBoosterPack() : base(RelicRarity.Shop)
    {
        WithCards(3);
        WithTip(SneckoKeyword.Offclass);
    }

    public override async Task AfterObtained()
    {
        var pools = Owner.GetOffclassCardPoolsForGift();
        var options = CardCreationOptions.ForNonCombatWithUniformOdds(pools, c => c.Rarity == CardRarity.Rare).WithFlags(CardCreationFlags.NoRarityModification);
        await RewardsCmd.OfferCustom(Owner, [new CardReward(options, DynamicVars.Cards.IntValue, Owner)]);
    }
}
