using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace DownfallU.DownfallUCode.Relics.Hermit;

public class CharredGlove : HermitRelic
{
    public CharredGlove() : base(RelicRarity.Common)
    {
        WithPower<VigorPower>(3, true);
    }

    public override async Task AfterCardDrawn(PlayerChoiceContext ctx, CardModel card, bool fromHandDraw)
    {
        if (card.Owner?.Creature != Owner?.Creature) return;
        if (card.Type == CardType.Curse)
        {
            Flash();
            await PowerCmd.Apply<VigorPower>(ctx, Owner!.Creature, DynamicVars["VigorPower"].BaseValue, Owner.Creature, null);
        }
    }
}
