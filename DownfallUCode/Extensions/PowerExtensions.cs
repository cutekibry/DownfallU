
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace DownfallU.DownfallUCode.Extensions;

public static class PowerExtensions
{
    public static bool IsActualDebuff<TPower>(this TPower power) where TPower : PowerModel
    {
        return (power.Type == PowerType.Debuff && power.Amount > 0) || (power.Type == PowerType.Buff && power.Amount < 0);
    }
}