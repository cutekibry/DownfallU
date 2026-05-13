using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace DownfallU.DownfallUCode.Powers.Snecko;

public abstract class SneckoPower : DownfallUPower
{
    public virtual Task AfterCardMuddled(PlayerChoiceContext choiceContext, CardModel card) => Task.CompletedTask;
}