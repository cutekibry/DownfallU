using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace DownfallU.DownfallUCode.Cards.Hermit;

[Pool(typeof(CurseCardPool))]
public class ImpendingDoom : HermitCard
{
    public override int MaxUpgradeLevel => 0;
    public override bool HasDeadOn => true;

    protected override bool ShouldGlowGoldInternal => false;
    protected override bool ShouldGlowRedInternal => IsDeadOn;
    public override bool HasTurnEndInHandEffect => IsDeadOn;

    public ImpendingDoom() : base(-2, CardType.Curse, CardRarity.Curse, TargetType.Self)
    {
        WithKeyword(CardKeyword.Unplayable);
        WithVar(new DamageVar(13, ValueProp.Unpowered));
    }

    protected override async Task OnTurnEndInHand(PlayerChoiceContext ctx)
    {
        ForceSetDeadOnForPlay(true);
        Creature[] targets = [Owner.Creature, ..CombatState!.HittableEnemies];
        foreach (Creature target in targets)
        {
            NFireBurstVfx child = NFireBurstVfx.Create(target, 0.75f)!;
            NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(child);
        }

        await CreatureCmd.Damage(ctx, Owner.Creature, DynamicVars.Damage, this);
        foreach (var enemy in CombatState!.HittableEnemies)
            await CreatureCmd.Damage(ctx, enemy, DynamicVars.Damage, this);

        await MaintanceDeadOnAfterPlayed(ctx, null);
        ClearIsDeadOnCache();
    }
}
