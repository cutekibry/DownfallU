using DownfallU.DownfallUCode.Powers.Hermit;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rooms;

namespace DownfallU.DownfallUCode.Relics.Hermit;

public class Spyglass : HermitRelic
{
    public Spyglass(): base(RelicRarity.Uncommon)
    {
        WithEnergy(1);
        WithTip(typeof(ConcentrationPower));
    }

    public override Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
    {
        if(power is ConcentrationPower && power.Owner == Owner.Creature)
        {
            if (amount + Owner.Creature.GetPowerAmount<ConcentrationPower>() == 0)
            {
                Status = RelicStatus.Normal;
            }
            else
                Status = RelicStatus.Active;
        }
        return Task.CompletedTask;
    }
    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side == Owner.Creature.Side && Owner.Creature.HasPower<ConcentrationPower>())
        {
            Flash();
            await PowerCmd.Apply<EnergyNextTurnPower>(choiceContext, Owner.Creature, DynamicVars.Energy.BaseValue, Owner.Creature, null);
        }
    }
    public override Task AfterCombatEnd(CombatRoom room)
    {
        Status = RelicStatus.Normal;
        return Task.CompletedTask;
    }
}
