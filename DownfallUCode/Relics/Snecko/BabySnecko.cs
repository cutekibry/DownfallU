using DownfallU.DownfallUCode.Actions;
using DownfallU.DownfallUCode.Character.Snecko;
using DownfallU.DownfallUCode.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;

namespace DownfallU.DownfallUCode.Relics.Snecko;

public class BabySnecko : SneckoRelic
{
    public BabySnecko() : base(RelicRarity.Event)
    {
        WithDamage(9);
        WithCards(2);
        WithTip(StaticHoverTip.Energy);
        WithTip(SneckoKeyword.Muddle);
    }

    public override async Task AfterPlayerTurnStartLate(PlayerChoiceContext ctx, Player player)
    {
        if (player == Owner)
        {
            Flash();
            var enemy = player.RandomEnemy();
            if (enemy != null)
            {
                await CreatureCmd.Damage(ctx, enemy, DynamicVars.Damage, Owner.Creature);
            }

            var candidates = player.GetHand().Where(c => c.CanBeMuddled() && c.EnergyCost.GetResolved() != 0);
            var targets = candidates.CombatRandomTake(2, player);
            await SneckoActions.Muddle(ctx, targets);
        }
    }

}