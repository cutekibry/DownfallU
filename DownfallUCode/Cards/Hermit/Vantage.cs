
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using BaseLib.Utils;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class Vantage : HermitCard
{
    public override bool HasDeadOn => true;

    public Vantage() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
        WithBlock(7, 2);
        WithCards(1, 1);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardBlock(this, play);
    }
    protected override async Task AfterPlayInternalIfDeadOn(PlayerChoiceContext ctx, CardPlay play)
    {
        var drawn = await CardPileCmd.Draw(ctx, DynamicVars.Cards.BaseValue, Owner);
        foreach (var card in drawn)
            if (card.IsUpgradable)
                CardCmd.Upgrade(card);
    }

}
