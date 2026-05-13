
using DownfallU.DownfallUCode.Powers.Hermit;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using BaseLib.Utils;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class Feint : HermitCard
{
    public Feint() : base(0, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
        WithBlock(3, 2);
        WithPower<BruisePower>(2, 1, true);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardBlock(this, play);

        foreach (var enemy in CombatState!.HittableEnemies)
            await PowerCmd.Apply<BruisePower>(ctx, enemy, DynamicVars["BruisePower"].IntValue, Owner.Creature, this);
    }

}
