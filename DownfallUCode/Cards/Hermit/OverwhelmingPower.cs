
using DownfallU.DownfallUCode.Powers.Hermit;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class OverwhelmingPower : HermitCard
{
    public OverwhelmingPower() : base(1, CardType.Power, CardRarity.Rare, TargetType.Self)
    {
        WithEnergy(3);
        WithCards(3, 1);
        WithHpLoss(4, -1);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await PlayerCmd.GainEnergy(DynamicVars.Energy.IntValue, Owner);
        await CardPileCmd.Draw(ctx, DynamicVars.Cards.IntValue, Owner, false);
        await PowerCmd.Apply<OverwhelmingPowerPower>(ctx, Owner.Creature, DynamicVars.HpLoss.IntValue, Owner.Creature, this);
    }

}
