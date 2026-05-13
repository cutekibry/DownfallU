using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;

namespace DownfallU.DownfallUCode.Relics.Hermit;

public class BrokenTooth : HermitRelic
{
    public BrokenTooth() : base(RelicRarity.Rare)
    {
        WithHeal(7);
        WithGold(35);
    }
    

    public override async Task AfterCombatVictory(CombatRoom room)
    {
        if(room.RoomType == RoomType.Elite)
        {
            Flash();
            await CreatureCmd.Heal(Owner.Creature, DynamicVars.Heal.BaseValue);
            await PlayerCmd.GainGold(DynamicVars.Gold.BaseValue, Owner);
        }
    }
}
