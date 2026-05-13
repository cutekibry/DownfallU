using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace DownfallU.DownfallUCode.Powers.Hermit;

/// <summary>
/// Lose X HP if you end your turn with unspent Energy.
/// </summary>
public class OverwhelmingPowerPower : HermitPower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task BeforeTurnEndEarly(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side != CombatSide.Player) return;

        // Only lose HP if ending turn with 0 energy
        var player = Owner.Player;
        if (player?.PlayerCombatState?.Energy != 0) return;

        Flash();
        await CreatureCmd.Damage(choiceContext, Owner, Amount,
            ValueProp.Unblockable | ValueProp.Unpowered, Owner, null);
    }
}
