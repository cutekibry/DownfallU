
using System.Security.AccessControl;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class Magnum : HermitCard
{

    public Magnum() : base(1, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
    {
        WithDamage(6, 2);
        WithCards(6);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        var handCount = PileType.Hand.GetPile(Owner).Cards.Count;
        var maxDiscard = Math.Min(DynamicVars.Cards.IntValue, handCount);

        var selected = (await CardSelectCmd.FromHandForDiscard(
            ctx,
            Owner,
            new CardSelectorPrefs(CardSelectorPrefs.DiscardSelectionPrompt, maxDiscard, maxDiscard),
            null,
            this
        )).ToList();

        await CardCmd.Discard(ctx, selected);
        await CommonActions.CardAttack(this, play, selected.Count).Execute(ctx);
    }

}
