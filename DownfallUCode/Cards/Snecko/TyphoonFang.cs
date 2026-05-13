using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Cards;
using DownfallU.DownfallUCode.Character.Snecko;
using DownfallU.DownfallUCode.Enchantments;
using DownfallU.DownfallUCode.Powers.Snecko;

namespace DownfallU.DownfallUCode.Cards.Snecko;

public class TyphoonFang : SneckoCard
{
    public override bool HasOverflow => true;

    public TyphoonFang() : base(1, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
    {
        WithDamage(12, 4);
        WithKeyword(SneckoKeyword.Overflow);
        WithTip(typeof(Fake));
        WithTips(c => [HoverTipFactory.FromCard<MinionDiveBomb>(c.IsUpgraded)]);
    }

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(ctx);
        if (IsOverflowed)
        {
            if (IsUpgraded)
                await PowerCmd.Apply<TyphoonPlusPower>(ctx, Owner.Creature, 1, Owner.Creature, this);
            else
                await PowerCmd.Apply<TyphoonPower>(ctx, Owner.Creature, 1, Owner.Creature, this);
        }
    }
}
