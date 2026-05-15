using DownfallU.DownfallUCode.Character.Snecko;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace DownfallU.DownfallUCode.Relics.Snecko;

public class LoadedDie : SneckoRelic
{
    public LoadedDie() : base(RelicRarity.Common)
    {
        WithTip(SneckoKeyword.Muddle);
        WithBlock(1);
    }
    public override async Task AfterCardMuddled(PlayerChoiceContext ctx, CardModel card)
    {
        if (card.Owner == Owner)
        {
            Flash();
            await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, null);
        }
    }
}
