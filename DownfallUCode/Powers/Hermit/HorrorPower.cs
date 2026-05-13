using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DownfallU.DownfallUCode.Powers.Hermit;

/// <summary>
/// Horror: Bruise does not wear off for X turns.
/// </summary>
public class HorrorPower : HermitPower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterTurnEndLate(PlayerChoiceContext ctx, CombatSide side)
    {
        if (side == Owner.Side)
            await PowerCmd.Apply<HorrorPower>(ctx, Owner, -1, Owner, null);
    }
}
