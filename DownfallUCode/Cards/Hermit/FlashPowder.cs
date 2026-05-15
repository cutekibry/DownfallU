
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using BaseLib.Utils;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class FlashPowder : HermitCard
{
    public FlashPowder() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithBlock(5);
        WithStrengthLoss(1, 1);
        WithKeyword(CardKeyword.Exhaust);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
    
        await CommonActions.CardBlock(this, play);

        foreach (Creature enemy in CombatState!.HittableEnemies)
        {
            await PowerCmd.Apply<StrengthPower>(ctx, enemy, -DynamicVars["StrengthPower"].BaseValue, Owner.Creature, this);
        }
    }

}
