using DownfallU.DownfallUCode.Character.Snecko;
using DownfallU.DownfallUCode.Enchantments;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace DownfallU.DownfallUCode.Relics.Snecko;

public class D8 : SneckoRelic
{
    public override bool HasUponPickupEffect => true;

    public D8() : base(RelicRarity.Event)
    {
        WithTip(SneckoKeyword.Overflow);
        WithCards(1);
        WithTip(typeof(Lead));
    }
    public override async Task AfterObtained()
    {
        foreach (CardModel item in await CardSelectCmd.FromDeckForEnchantment(prefs: new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, DynamicVars.Cards.IntValue), player: Owner, enchantment: ModelDb.Enchantment<Lead>(), amount: DynamicVars.Cards.IntValue))
        {
            CardCmd.Enchant<Lead>(item, 1m);
            var nCardEnchantVfx = NCardEnchantVfx.Create(item);
            if (nCardEnchantVfx != null)
            {
                NRun.Instance?.GlobalUi.CardPreviewContainer.AddChildSafely(nCardEnchantVfx);
            }
        }
    }

}
