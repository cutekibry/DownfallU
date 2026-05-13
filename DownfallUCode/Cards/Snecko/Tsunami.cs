using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using DownfallU.DownfallUCode.Character.Snecko;
using DownfallU.DownfallUCode.Powers.Snecko;

namespace DownfallU.DownfallUCode.Cards.Snecko;

public class Tsunami : SneckoCard
{
    public Tsunami() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self)
    {
        WithPower<TsunamiPower>(4, 1, false);
        WithTip(StaticHoverTip.Block);
        WithKeyword(SneckoKeyword.Overflow);
    }

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await PowerCmd.Apply<TsunamiPower>(ctx, Owner.Creature, DynamicVars["TsunamiPower"].BaseValue, Owner.Creature, this);
    }
}
