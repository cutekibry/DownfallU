using BaseLib.Utils;
using DownfallU.DownfallUCode.Extensions;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Events;
using MegaCrit.Sts2.Core.Models.RelicPools;

namespace DownfallU.DownfallUCode.Relics.Snecko;

[Pool(typeof(SharedRelicPool))]
public class SneckoTalon : SneckoRelic
{
    public SneckoTalon() : base(RelicRarity.Ancient)
    {
        WithAncient<Darv>();
        WithEnergy(1);
    }
    public override Task AfterPlayerTurnStartLate(PlayerChoiceContext ctx, Player player)
    {
        var muddledCards = player.GetHand().Where(c => c.CanBeMuddled() && c.EnergyCost.GetResolved() != 0).ToList();
        if (muddledCards.Count == 0)
            return Task.CompletedTask;
        
        var selected = muddledCards.CombatRandomTake(1, player).First();
        selected.EnergyCost.AddThisTurnOrUntilPlayed(-DynamicVars.Energy.IntValue);
        return Task.CompletedTask;
    }
}
