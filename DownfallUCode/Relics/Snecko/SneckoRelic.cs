
using BaseLib.Utils;
using DownfallU.DownfallUCode.Character.Snecko;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace DownfallU.DownfallUCode.Relics.Snecko;

[Pool(typeof(SneckoRelicPool))]
public abstract class SneckoRelic(RelicRarity rarity) : DownfallURelic(rarity)
{
    public override string CharacterID => "Snecko";
    public virtual Task AfterCardMuddled(PlayerChoiceContext choiceContext, CardModel card) => Task.CompletedTask;
}