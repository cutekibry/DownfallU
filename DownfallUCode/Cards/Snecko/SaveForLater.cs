using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using DownfallU.DownfallUCode.Powers.Hermit;

namespace DownfallU.DownfallUCode.Cards.Snecko;

public class SaveForLater : SneckoCard
{
    public SaveForLater() : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
    {
        WithDamage(8, 3);
        WithPower<CoalescencePower>(1, 1, false);
        WithTips(_ => [HoverTipFactory.FromKeyword(CardKeyword.Retain)]);
    }

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(ctx);
        await PowerCmd.Apply<CoalescencePower>(ctx, Owner.Creature, DynamicVars["CoalescencePower"].BaseValue, Owner.Creature, this);
    }
}
