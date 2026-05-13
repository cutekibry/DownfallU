
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.ValueProps;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class RoundhouseKick : HermitCard
{
    public RoundhouseKick() : base(2, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies)
    {
        WithDamage(13, 5);
        WithKeyword(CardKeyword.Exhaust);
        WithTips(_ => [StunIntent.GetStaticHoverTip()]);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(ctx);
        foreach (var enemy in CombatState!.HittableEnemies)
        {
            var monster = enemy.Monster!;
            if (!monster.IntendsToAttack)
            {
                await CreatureCmd.Stun(enemy);
            }
        }
    }

}
