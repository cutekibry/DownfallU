using BaseLib.Utils;
using DownfallU.DownfallUCode.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.ValueProps;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class ItchyTrigger : HermitCard
{
    public override bool HasDeadOn => true;

    public ItchyTrigger() : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
    {
        WithDamage(7, 2);
        WithVar("CostReduction", 1, 1);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(ctx);
    }
    protected override Task AfterPlayInternalIfDeadOn(PlayerChoiceContext ctx, CardPlay play)
    {
        var hand = Owner.GetHand();
        var card = Owner.RunState.Rng.CombatCardSelection.NextItem(hand.Where(c => c.CanBeMuddled()));
        card?.EnergyCost.AddThisTurnOrUntilPlayed(-(int)DynamicVars["CostReduction"].BaseValue, reduceOnly: true);
        return Task.CompletedTask;
    }

}
