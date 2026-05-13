
using BaseLib.Utils;
using DownfallU.DownfallUCode.Character.Hermit;
using DownfallU.DownfallUCode.Utils.Hermit;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class CalledShot : HermitCard
{
    protected override bool ShouldGlowGoldInternal => DeadOnCounter.GetIsLastCardDeadOn(Owner);

    public CalledShot() : base(0, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
    {
        WithDamage(5, 2);
        WithCards(1);
        WithKeyword(HermitKeywords.DeadOn);
        WithKeyword(CardKeyword.Retain, UpgradeType.Add);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(ctx);

        if (DeadOnCounter.GetIsLastCardDeadOn(Owner))
        {
            await CardPileCmd.Draw(ctx, DynamicVars.Cards.BaseValue, Owner, false);
        }
    }
}
