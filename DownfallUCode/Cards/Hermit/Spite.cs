
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class Spite : HermitCard
{
    private const int BlockAmount = 8;
    private const int UpgradedBlockAmount = 10;
    private const int DrawAmount = 3;
    private const int UpgradedDrawAmount = 4;

    public Spite() : base(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithBlock(8, 2);
        WithCards(3, 1);
        WithTip(CardKeyword.Unplayable);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        var unplayables = PileType.Hand.GetPile(Owner).Cards
            .Where(c => c.Keywords.Contains(CardKeyword.Unplayable));
        foreach (var card in unplayables)
        {
            await CardCmd.Exhaust(ctx, card);
        }

        await CommonActions.CardBlock(this, play);
        await CardPileCmd.Draw(ctx, DynamicVars.Cards.IntValue, Owner);
    }

}
