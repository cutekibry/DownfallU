using BaseLib.Utils;
using DownfallU.DownfallUCode.Powers.Hermit;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class Headshot : HermitCard
{
    public override bool HasDeadOn => true;

    public Headshot() : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
    {
        WithDamage(7, 2);
    }

    public override decimal ModifyDamageMultiplicative(Creature? target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
    {
        if (cardSource != this || dealer != Owner.Creature || !props.IsPoweredAttack() || !IsDeadOn)
            return 1m;

        if (Owner.Creature.HasPower<SnipePower>())
            return 4m;
        return 2m;
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(ctx);
    }

}
