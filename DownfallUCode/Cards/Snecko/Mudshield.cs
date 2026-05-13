using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using DownfallU.DownfallUCode.Character.Snecko;
using DownfallU.DownfallUCode.Powers.Snecko;

namespace DownfallU.DownfallUCode.Cards.Snecko;

// Reference: STS1 Mudshield; local CheapStock/BlunderGuard for applying custom powers.
public class Mudshield : SneckoCard
{
    public Mudshield() : base(1, CardType.Power, CardRarity.Rare, TargetType.Self)
    {
        WithPower<MudshieldPower>(2, 1, false);
        WithTip(StaticHoverTip.Block);
        WithKeyword(SneckoKeyword.Muddle);
    }

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await PowerCmd.Apply<MudshieldPower>(ctx, Owner.Creature, DynamicVars["MudshieldPower"].BaseValue, Owner.Creature, this);
    }
}
