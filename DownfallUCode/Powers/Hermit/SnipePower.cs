using DownfallU.DownfallUCode.Utils.Hermit;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DownfallU.DownfallUCode.Powers.Hermit;

public class SnipePower : HermitPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterDeadOnTriggered(PlayerChoiceContext ctx, CardPlay? play)
    {
        if (play?.Card.Type != CardType.Attack && play?.Card.Type != CardType.Skill)
            return;
        DeadOnCounter.IncreaseCounter(Owner.Player!);
        await PowerCmd.Apply<SnipePower>(ctx, Owner, -1, Owner, play.Card);
    }

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side != Owner.Side) return;
        await PowerCmd.Remove(this);
    }
}
