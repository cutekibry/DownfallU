using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using DownfallU.DownfallUCode.Cards.Snecko;
using MegaCrit.Sts2.Core.Models;

namespace DownfallU.DownfallUCode.Relics.Snecko;


public class SneckoSoul : SneckoCoreRelic
{
    public SneckoSoul() : base(RelicRarity.Starter)
    {
        WithTip(typeof(SoulRoll));
    }

    public override async Task BeforeHandDraw(Player player, PlayerChoiceContext ctx, ICombatState combatState)
    {
        if (player == Owner && combatState.RoundNumber == 1)
            await CardPileCmd.AddGeneratedCardToCombat(Owner.Creature.CombatState!.CreateCard<SoulRoll>(Owner), PileType.Hand, Owner);
    }
    public override RelicModel GetUpgradeReplacement()
    {
        return ModelDb.Relic<SuperSneckoSoul>();
    }
}
