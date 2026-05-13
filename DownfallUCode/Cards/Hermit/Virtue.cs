using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class Virtue : HermitCard
{

    public Virtue() : base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithKeyword(CardKeyword.Retain);
        WithVar("Reduce", 1, 1);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        int reduceBy = DynamicVars["Reduce"].IntValue;
        foreach (var power in Owner.Creature.Powers)
        {
            if (power.StackType == PowerStackType.Counter) {
                if (power.Type == PowerType.Debuff && power.Amount > 0)
                {
                    if (power.Amount <= reduceBy)
                        await PowerCmd.Remove(power);
                    else
                        power.SetAmount(power.Amount - reduceBy);
                }
                else if(power.Type == PowerType.Buff && power.Amount < 0)
                {
                    if (power.Amount >= -reduceBy)
                        await PowerCmd.Remove(power);
                    else
                        power.SetAmount(power.Amount + reduceBy);
                }
            }
        }
    }

}
