using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using BaseLib.Utils;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class GhostlyPresence : HermitCard
{
    public override bool HasDeadOn => true;

    public GhostlyPresence() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithBlock(8, 4);
        WithPower<WeakPower>(1, 1, true);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardBlock(this, play);
    }
    protected override async Task AfterPlayInternalIfDeadOn(PlayerChoiceContext ctx, CardPlay play)
    {
        foreach (Creature enemy in CombatState!.HittableEnemies)
            await PowerCmd.Apply<WeakPower>(ctx, enemy, DynamicVars["WeakPower"].BaseValue, Owner.Creature, this);
    }

}
