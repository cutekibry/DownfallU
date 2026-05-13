using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace DownfallU.DownfallUCode.Relics.Hermit;

public class Horseshoe : HermitRelic
{
    public Horseshoe() : base(RelicRarity.Common)
    {
        WithTip(typeof(WeakPower));
        WithTip(typeof(FrailPower));
        WithTip(typeof(VulnerablePower));
    }

    public override bool TryModifyPowerAmountReceived(
        PowerModel canonicalPower,
        Creature target,
        decimal amount,
        Creature? applier,
        out decimal modifiedAmount)
    {
        modifiedAmount = amount;

        // Only reduce debuffs being applied to the relic owner
        if (target != Owner?.Creature)
            return false;

        // Only trigger for positive amounts (application, not removal)
        if (amount <= 0m)
            return false;

        // Only intercept Weak, Frail, or Vulnerable
        if (canonicalPower is not (WeakPower or FrailPower or VulnerablePower))
            return false;

        // Reduce by 1, minimum 0
        modifiedAmount = Math.Max(0m, amount - 1m);
        return true;
    }
}

internal class PowerHoverTip
{
    private Type type;

    public PowerHoverTip(Type type)
    {
        this.type = type;
    }
}