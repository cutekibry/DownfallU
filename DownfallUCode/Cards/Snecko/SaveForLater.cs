using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using DownfallU.DownfallUCode.Powers.Snecko;

namespace DownfallU.DownfallUCode.Cards.Snecko;

// Reference: STS1 SaveForLater; HermitMod-STS2 CoalescencePower and STS2 WellLaidPlansPower for Retain selection.
public class SaveForLater : SneckoCard
{
    public SaveForLater() : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
    {
        WithDamage(8, 3);
        WithPower<SaveForLaterPower>(1, 1, false);
        WithTips(_ => [HoverTipFactory.FromKeyword(CardKeyword.Retain)]);
    }

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(ctx);
        await PowerCmd.Apply<SaveForLaterPower>(ctx, Owner.Creature, DynamicVars["SaveForLaterPower"].BaseValue, Owner.Creature, this);
    }
}
