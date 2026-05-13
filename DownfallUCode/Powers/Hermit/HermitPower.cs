using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace DownfallU.DownfallUCode.Powers.Hermit;

public abstract class HermitPower : DownfallUPower
{
    public override string CharacterId => "Hermit";
    public virtual Task AfterDeadOnTriggered(PlayerChoiceContext playerChoiceContext, CardPlay? cardPlay) => Task.CompletedTask;
}
