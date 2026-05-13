using HarmonyLib;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Potions;
using MegaCrit.Sts2.Core.Rooms;

namespace DownfallU.DownfallUCode.Relics.Hermit;

public class Shotglass : HermitRelic
{
    public Shotglass(): base(RelicRarity.Uncommon)
    {
        WithVar("Limit", 2);
    }

    public int AvailableUses { get; private set; } = 0;
    public bool IsInCombat { get; private set; } = false;

    public override bool ShowCounter => IsInCombat;
    public override int DisplayAmount => AvailableUses;

    public override Task BeforeCombatStart()
    {
        AvailableUses = (int)DynamicVars["Limit"].BaseValue;
        IsInCombat = true;
        InvokeDisplayAmountChanged();
        return Task.CompletedTask;
    }

    public override async Task AfterPotionUsed(PotionModel potion, Creature? target)
    {
        if(potion.Owner != Owner || AvailableUses == 0 || !IsInCombat) 
            return;
        AvailableUses--;
        Flash();
        await PotionCmd.TryToProcure(PotionFactory.CreateRandomPotionInCombat(Owner, Owner.RunState.Rng.CombatPotionGeneration).ToMutable(), Owner);
        InvokeDisplayAmountChanged();

        if (AvailableUses == 0) 
            Status = RelicStatus.Disabled;
    }

    public override Task AfterCombatEnd(CombatRoom room)
    {
        IsInCombat = false;
        Status = RelicStatus.Normal;
        InvokeDisplayAmountChanged();
        return Task.CompletedTask;
    }

    [HarmonyPatch(typeof(NPotionPopup), "RefreshButtons")]
    private static class ShotglassPatch
    {
        private static readonly AccessTools.FieldRef<NPotionPopup, NPotionHolder> HolderRef =
            AccessTools.FieldRefAccess<NPotionPopup, NPotionHolder>("_holder");

        private static readonly AccessTools.FieldRef<NPotionPopup, NPotionPopupButton> UseButtonRef =
            AccessTools.FieldRefAccess<NPotionPopup, NPotionPopupButton>("_useButton");

        [HarmonyPostfix]
        public static void Postfix(NPotionPopup __instance)
        {
            DisableUseButtonIfShotglassIsSpent(__instance);
        }

        public static void DisableUseButtonIfShotglassIsSpent(NPotionPopup instance)
        {
            PotionModel? potion = HolderRef(instance)?.Potion?.Model;
            Shotglass? shotglass = potion?.Owner?.GetRelic<Shotglass>();

            if (shotglass != null && shotglass.IsInCombat && shotglass.AvailableUses == 0)
                UseButtonRef(instance)?.Disable();
        }
    }

    [HarmonyPatch(typeof(NPotionPopup), nameof(NPotionPopup._Ready))]
    public static class ShotglassPotionPopupReadyPatch
    {
        [HarmonyPostfix]
        public static void Postfix(NPotionPopup __instance)
        {
            ShotglassPatch.DisableUseButtonIfShotglassIsSpent(__instance);
        }
    }

}
