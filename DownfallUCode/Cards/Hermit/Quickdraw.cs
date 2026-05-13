
using BaseLib.Utils;
using DownfallU.DownfallUCode.Powers.Hermit;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class Quickdraw : HermitCard
{
    public Quickdraw() : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
    {
        WithDamage(9, 2);
        WithCards(2, 1);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(ctx);
        await CardPileCmd.Draw(ctx, DynamicVars.Cards.IntValue, Owner);
        await PowerCmd.Apply<DrawFewerCardsNextTurnPower>(ctx, Owner.Creature, 1, Owner.Creature, this);
    }

}
