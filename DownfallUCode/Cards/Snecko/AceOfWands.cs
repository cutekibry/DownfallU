using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using DownfallU.DownfallUCode.Powers.Snecko;
using DownfallU.DownfallUCode.Utils.Snecko;

namespace DownfallU.DownfallUCode.Cards.Snecko;

public class AceOfWands : SneckoCard
{
    public override Func<CardModel, bool>? GiftFilter => GiftSelector.IsDebuff;  
    public AceOfWands() : base(3, CardType.Power, CardRarity.Rare, TargetType.Self)
    {
        WithPower<AceOfWandsPower>(4, false);
        WithKeyword(CardKeyword.Ethereal, UpgradeType.Remove);
        WithTip(StaticHoverTip.Block);
    }

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await PowerCmd.Apply<AceOfWandsPower>(ctx, Owner.Creature, DynamicVars["AceOfWandsPower"].BaseValue, Owner.Creature, this);
    }
}