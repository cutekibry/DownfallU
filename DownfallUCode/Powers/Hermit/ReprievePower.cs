using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace DownfallU.DownfallUCode.Powers.Hermit;

/// <summary>
/// Take X extra turn(s) after this one.
/// </summary>
public class ReprievePower : HermitPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
}
