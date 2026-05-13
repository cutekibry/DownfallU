using DownfallU.DownfallUCode.Character.Hermit;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DownfallU.DownfallUCode.Relics.Hermit;

public class BlackPowder : HermitRelic
{
    public BlackPowder() : base(RelicRarity.Uncommon)
    {
        WithDamage(2);
        WithTip(HermitKeywords.DeadOn);
    }

    public override async Task AfterDeadOnTriggered(PlayerChoiceContext ctx, CardPlay? play)
    {
        await CreatureCmd.Damage(ctx, Owner.Creature.CombatState!.HittableEnemies, DynamicVars.Damage, Owner.Creature);
    }


}
