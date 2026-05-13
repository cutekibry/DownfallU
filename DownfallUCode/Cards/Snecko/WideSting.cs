using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using DownfallU.DownfallUCode.Character.Snecko;
using DownfallU.DownfallUCode.Extensions;

namespace DownfallU.DownfallUCode.Cards.Snecko;

// Reference: STS1 WideSting; local IronFang for all-enemy attacks and SneckoSoul/GiftSelector for Gift Common.
public class WideSting : SneckoCard
{
    public override Func<CardModel, bool>? GiftFilter => c => c.Rarity == CardRarity.Common;

    public WideSting() : base(1, CardType.Attack, CardRarity.Common, TargetType.AllEnemies)
    {
        WithDamage(7, 3);
        WithKeyword(SneckoKeyword.Offclass);
    }

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(ctx);

        var offclassCards = PileType.Hand.GetPile(Owner).Cards
            .Where(c => c.IsOffclass() && c.IsUpgradable)
            .ToList();
        CardCmd.Upgrade(offclassCards, CardPreviewStyle.None);
    }
}
