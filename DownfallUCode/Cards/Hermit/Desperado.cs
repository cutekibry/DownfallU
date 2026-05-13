
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class Desperado : HermitCard
{
    public Desperado() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
        WithDamage(10, 2);
        WithVar("PlayCountMultiplier", 1);
    }

    public override decimal ModifyDamageMultiplicative(Creature? target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
    {
        if (cardSource != this || dealer != Owner.Creature || !props.IsPoweredAttack())
            return 1m;

        return DynamicVars["PlayCountMultiplier"].BaseValue;
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(ctx);
        DynamicVars["PlayCountMultiplier"].BaseValue *= 2;
        EnergyCost.AddThisCombat(1);
    }

}
