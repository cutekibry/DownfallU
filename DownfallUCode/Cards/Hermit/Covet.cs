
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public sealed class Covet : HermitCard
{

    public Covet() : base(0, CardType.Skill, CardRarity.Basic, TargetType.Self)
    {
        WithCards(1, 1);
        WithTip(CardKeyword.Exhaust);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        var selected = (await CardSelectCmd.FromHandForDiscard(
            ctx,
            Owner,
            new CardSelectorPrefs(CardSelectorPrefs.DiscardSelectionPrompt, 1),
            null,
            this
        )).FirstOrDefault();

        if (selected != null)
        {
            bool isCurse = selected.Type == CardType.Curse;

            if (isCurse)
            {
                await CardCmd.Exhaust(ctx, selected);
            }
            else
            {
                await CardCmd.Discard(ctx, selected);
            }
        }

        await CardPileCmd.Draw(ctx, DynamicVars.Cards.IntValue, Owner, false);
    }
}
