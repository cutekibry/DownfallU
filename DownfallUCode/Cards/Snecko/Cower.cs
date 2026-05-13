using BaseLib.Utils;
using DownfallU.DownfallUCode.Cards.Hermit;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DownfallU.DownfallUCode.Cards.Snecko;

public class Cower : SneckoCard
{
    public Cower() : base(2, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
        WithBlock(14, 4);
        WithKeyword(CardKeyword.Exhaust);
        WithUpgradingCardTip<HoleUp>();
    }

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardBlock(this, play);
        var card = CombatState!.CreateCard<HoleUp>(Owner);
        if (IsUpgraded && card.IsUpgradable)
            CardCmd.Upgrade(card);
        await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, Owner);
    }
}
