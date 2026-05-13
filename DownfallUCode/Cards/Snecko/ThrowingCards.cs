using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Cards;

namespace DownfallU.DownfallUCode.Cards.Snecko;

// Reference: STS1 ThrowingCards; STS2 generated-card creation pattern from Lacerate/Deception.
public class ThrowingCards : SneckoCard
{
    protected override bool HasEnergyCostX => true;

    public ThrowingCards() : base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithKeyword(CardKeyword.Exhaust);
        WithEnergy(1);
        WithUpgradingCardTip<Ftl>();
    }

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        for (var i = 0; i < ResolveEnergyXValue(); i++)
        {
            var card = CombatState!.CreateCard<Ftl>(Owner);
            if (IsUpgraded)
                CardCmd.Upgrade(card);
            await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, Owner);
        }

        await PlayerCmd.GainEnergy(DynamicVars.Energy.IntValue, Owner);
    }
}
