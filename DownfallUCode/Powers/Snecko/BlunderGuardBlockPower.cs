using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace DownfallU.DownfallUCode.Powers.Snecko;


public class BlunderGuardBlockPower : SneckoPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new EnergyVar(3)];

    public override async Task BeforeCardPlayed(CardPlay play)
    {
        if (play.Card.Owner.Creature == Owner && play.Card.EnergyCost.GetResolved() >= DynamicVars.Energy.IntValue)
        {
            Flash();
            await CreatureCmd.GainBlock(Owner, Amount, ValueProp.Unpowered, null);
        }
    }
}