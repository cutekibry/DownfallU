using DownfallU.DownfallUCode.Powers.Hermit;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using DownfallU.DownfallUCode.Actions;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class EyeOfTheStorm : HermitCard
{
    public EyeOfTheStorm() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithKeyword(CardKeyword.Exhaust);
        WithPower<ConcentrationPower>(1);
        WithCostUpgradeBy(-1);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await ShortActions.CardPower<ConcentrationPower>(this, play, ctx);

        int gain = Owner.PlayerCombatState!.MaxEnergy - Owner.PlayerCombatState.Energy;
        if (gain > 0)
            await PlayerCmd.GainEnergy(gain, Owner);
    }

}
