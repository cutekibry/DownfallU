using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace DownfallU.DownfallUCode.Relics.Hermit;

public class RyeStalk : HermitRelic
{
    public RyeStalk() : base(RelicRarity.Common)
    {
        
    }

    public override async Task AfterDamageReceived(PlayerChoiceContext ctx, Creature target, DamageResult result, ValueProp props, Creature? dealer, CardModel? cardSource)
    {
        if (target == Owner.Creature && result.UnblockedDamage > 0 && props.HasFlag(ValueProp.Move) && dealer != null && dealer.IsEnemy)
        {
            Flash();
            await CardPileCmd.Draw(ctx, Owner);
        }
    }
}
