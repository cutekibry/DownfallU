
using DownfallU.DownfallUCode.Powers.Hermit;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class Combo : HermitCard
{
    public Combo() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self)
    {
        WithPower<ComboPower>(1, 1, false);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await PowerCmd.Apply<ComboPower>(ctx, Owner.Creature, DynamicVars["ComboPower"].BaseValue, Owner.Creature, this);
    }
}
