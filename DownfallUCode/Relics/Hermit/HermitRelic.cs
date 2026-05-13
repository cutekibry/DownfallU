using BaseLib.Utils;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Entities.Cards;
using DownfallU.DownfallUCode.Character.Hermit;
using MegaCrit.Sts2.Core.Entities.Relics;

namespace DownfallU.DownfallUCode.Relics.Hermit;

[Pool(typeof(HermitRelicPool))]
public abstract class HermitRelic(RelicRarity rarity) : DownfallURelic(rarity)
{
    public override string CharacterID => "Hermit";
    public virtual Task AfterDeadOnTriggered(PlayerChoiceContext playerChoiceContext, CardPlay? cardPlay) => Task.CompletedTask;
}