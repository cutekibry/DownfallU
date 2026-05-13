
using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class BodyArmor : HermitCard
{
    public BodyArmor() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
        WithBlock(5, 2);
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
            bool wasNonAttack = selected.Type != CardType.Attack;
            await CardCmd.Discard(ctx, selected);
            await CommonActions.CardBlock(this, play);
            if (wasNonAttack)
            {
                await CommonActions.CardBlock(this, play);
            }
        }
    }
}
