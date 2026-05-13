using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using DownfallU.DownfallUCode.Character.Snecko;
using DownfallU.DownfallUCode.Actions;

namespace DownfallU.DownfallUCode.Cards.Snecko;

// Reference: STS1 SoulDraw; STS2 Discovery/Splash for random generated cards and CardCmd.ApplyKeyword for Retain.
public class SoulDraw : SneckoCard
{
    public SoulDraw() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithCards(2);
        WithKeyword(CardKeyword.Exhaust);
        WithTip(CardKeyword.Retain);
        WithKeyword(SneckoKeyword.Offclass);
        WithCostUpgradeBy(-1);
    }

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        var cards = SneckoActions.GenerateRandomOffclassCards(Owner, DynamicVars.Cards.IntValue);

        foreach (var card in cards)
        {
            CardCmd.ApplyKeyword(card, CardKeyword.Retain);
            await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, Owner);
        }
    }
}
