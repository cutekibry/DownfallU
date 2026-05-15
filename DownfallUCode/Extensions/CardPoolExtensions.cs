
using DownfallU.DownfallUCode.Character.Snecko;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace DownfallU.DownfallUCode.Extensions;

public static class CardPoolExtensions
{
    public static string ColorId<TPool>(this TPool pool) where TPool : CardPoolModel
    {
        return (pool is ColorlessCardPool || pool is TokenCardPool || pool is StatusCardPool) ? "_colorless_" : pool.Id.Entry;
    }

    public static bool IsOffclass(this CardPoolModel pool)
    {
        return pool is not SneckoCardPool;
    }
}