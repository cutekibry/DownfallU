
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Enchantments;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class TakeCover : HermitCard
{
    protected override bool HasEnergyCostX => true;

    public TakeCover() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithKeyword(CardKeyword.Exhaust);
        WithUpgradingCardTip<DefendHermit>();
        WithTip(typeof(Nimble));
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        var defend = CombatState!.CreateCard<DefendHermit>(Owner);
        defend.SetToFreeThisCombat();
        if(IsUpgraded)
            CardCmd.Upgrade(defend);
        CardCmd.Enchant<Nimble>(defend, 3 * EnergyCost.GetResolved());
        await CardPileCmd.AddGeneratedCardToCombat(defend, PileType.Hand, Owner);
    }
}
