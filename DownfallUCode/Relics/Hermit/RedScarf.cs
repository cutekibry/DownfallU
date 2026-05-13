using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Models;

namespace DownfallU.DownfallUCode.Relics.Hermit;

public class RedScarf : HermitRelic
{
    public RedScarf() : base(RelicRarity.Common)
    {
        WithBlock(3);
    }

    public override async Task BeforePowerAmountChanged(PowerModel power, decimal amount, Creature target, Creature? applier, CardModel? cardSource)
    {
        if(amount > 0 && target.IsEnemy && power.Type == PowerType.Debuff && (target.GetPower(power.Id)?.Amount ?? 0) == 0 && applier == Owner.Creature)
        {
            Flash();
            await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, null);
        }
    }
}
