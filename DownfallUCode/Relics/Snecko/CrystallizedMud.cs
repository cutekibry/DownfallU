using DownfallU.DownfallUCode.Character.Snecko;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Models.Events;
using SneckoCharacter = DownfallU.DownfallUCode.Character.Snecko.Snecko;

namespace DownfallU.DownfallUCode.Relics.Snecko;

public class CrystallizedMud : SneckoRelic
{
    public CrystallizedMud() : base(RelicRarity.Ancient)
    {
        WithEnergy(1);
        WithAncient<Vakuu, SneckoCharacter>();
        WithTip(SneckoKeyword.Muddle);
    }
    public override decimal ModifyMaxEnergy(Player player, decimal amount)
    {
        if (player != Owner)
        {
            return amount;
        }

        return amount + DynamicVars.Energy.IntValue;
    }
}
