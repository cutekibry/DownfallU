using DownfallU.DownfallUCode.Character.Snecko;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Models.Events;
using SneckoCharacter = DownfallU.DownfallUCode.Character.Snecko.Snecko;

namespace DownfallU.DownfallUCode.Relics.Snecko;

public class CleanMud : SneckoRelic
{
    public CleanMud() : base(RelicRarity.Ancient)
    {
        WithEnergy(3);
        WithAncient<Tezcatara, SneckoCharacter>();
        WithTip(SneckoKeyword.Muddle);
    }
}
