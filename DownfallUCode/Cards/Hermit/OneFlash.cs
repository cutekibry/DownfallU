using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.ValueProps;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class OneFlash : HermitCard
{
    public override bool HasDeadOn => true;

    public OneFlash() : base(1, CardType.Attack, CardRarity.Ancient, TargetType.AnyEnemy)
    {
        WithDamage(10, 3);
        WithPower<StrengthPower>(3, 1, true);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        IRunState runState = IRunState.GetFrom([play.Target!, Owner.Creature]);
        decimal modifiedDamage = Hook.ModifyDamage(runState, CombatState, play.Target!, Owner.Creature, DynamicVars.Damage.BaseValue, ValueProp.Move, this, ModifyDamageHookType.All, CardPreviewMode.None, out _);
        int unblockedDamage = Math.Max(0, (int)(modifiedDamage - play.Target!.Block));
        await CreatureCmd.GainBlock(Owner.Creature, unblockedDamage, ValueProp.Move, play);

        await CommonActions.CardAttack(this, play).Execute(ctx);
    }
    protected override async Task AfterPlayInternalIfDeadOn(PlayerChoiceContext ctx, CardPlay play)
    {
        await PowerCmd.Apply<StrengthPower>(ctx, Owner.Creature, DynamicVars["StrengthPower"].IntValue, Owner.Creature, this);
    }

}
