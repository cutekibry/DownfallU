using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using DownfallU.DownfallUCode.Character.Snecko;
using DownfallU.DownfallUCode.Powers.Snecko;

namespace DownfallU.DownfallUCode.Cards.Snecko;

// Reference: STS1 OverwhelmingPresence.
public class OverwhelmingPresence : SneckoCard
{
    public OverwhelmingPresence() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self)
    {
        WithPower<OverwhelmingPresencePower>(1, false);
        WithKeyword(CardKeyword.Ethereal, UpgradeType.Remove);
        WithKeyword(SneckoKeyword.Offclass);
    }

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await PowerCmd.Apply<OverwhelmingPresencePower>(ctx, Owner.Creature, DynamicVars["OverwhelmingPresencePower"].BaseValue, Owner.Creature, this);
    }
}
