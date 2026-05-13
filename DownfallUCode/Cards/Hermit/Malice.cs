using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class Malice : HermitCard
{
    public Malice() : base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
        WithDamage(16, 4);
        WithTip(CardKeyword.Exhaust);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        // Prompt the player to exhaust a card from hand
        var card = (await CardSelectCmd.FromHand(prefs: new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, 1), context: ctx, player: Owner, filter: null, source: this)).FirstOrDefault();
        if (card != null)
            await CardCmd.Exhaust(ctx, card);

        if (card?.Type == CardType.Curse)
        {
            // Exhausted a Curse — deal damage to ALL enemies
            await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
                .FromCard(this)
                .TargetingAllOpponents(CombatState!)
                .Execute(ctx);
        }
        else
        {
            // Normal — deal damage to the single target
            await CommonActions.CardAttack(this, play).Execute(ctx);
        }
    }

}
