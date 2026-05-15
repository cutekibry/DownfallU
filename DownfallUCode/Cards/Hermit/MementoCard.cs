using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace DownfallU.DownfallUCode.Cards.Hermit;

[Pool(typeof(CurseCardPool))]
public class MementoCard : HermitCard
{
    public override int MaxUpgradeLevel => 0;
    public override bool CanBeGeneratedInCombat => false;

    public MementoCard() : base(0, CardType.Curse, CardRarity.Curse, TargetType.Self)
    {
        WithKeyword(CardKeyword.Retain);
        WithPower<VulnerablePower>(1, true);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        foreach (var enemy in CombatState!.HittableEnemies)
        {
            await PowerCmd.Apply<VulnerablePower>(ctx, enemy, DynamicVars["VulnerablePower"].BaseValue, Owner.Creature, this);
        }

        await PowerCmd.Apply<VulnerablePower>(ctx, Owner.Creature, DynamicVars["VulnerablePower"].BaseValue, Owner.Creature, this);
    }
}
