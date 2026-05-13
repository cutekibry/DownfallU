using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.ValueProps;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class Snapshot : HermitCard, ITranscendenceCard
{
    public override bool HasDeadOn => true;

    public Snapshot() : base(1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
    {
        WithDamage(6, 2);
    }

    protected override async Task BeforePlayInternalIfDeadOn(PlayerChoiceContext ctx, CardPlay play)
    {
        IRunState runState = IRunState.GetFrom([play.Target!, Owner.Creature]);
        decimal modifiedDamage = Hook.ModifyDamage(runState, CombatState, play.Target!, Owner.Creature, DynamicVars.Damage.BaseValue, ValueProp.Move, this, ModifyDamageHookType.All, CardPreviewMode.None, out _);
        int unblockedDamage = (int)(modifiedDamage - play.Target!.Block);
        await CreatureCmd.GainBlock(Owner.Creature, unblockedDamage, ValueProp.Move, play);
    }
    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(ctx);
    }

    public CardModel GetTranscendenceTransformedCard()
    {
        return ModelDb.Card<OneFlash>();
    }
}
