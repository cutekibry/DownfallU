using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class BlackWind : HermitCard
{
    public BlackWind() : base(2, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
    {
        WithCalculatedDamage(0, GetLoseHp);
        WithKeywords(CardKeyword.Ethereal, CardKeyword.Exhaust);
        WithCostUpgradeBy(-1);
    }

    private static decimal GetLoseHp(CardModel card, Creature? _)
    {
        return card.Owner.Creature.MaxHp - card.Owner.Creature.CurrentHp;
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(ctx);
    }
}
