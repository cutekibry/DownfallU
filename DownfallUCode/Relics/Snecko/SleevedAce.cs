using DownfallU.DownfallUCode.Cards.Snecko;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DownfallU.DownfallUCode.Relics.Snecko;

public class SleevedAce : SneckoRelic
{
    public SleevedAce() : base(RelicRarity.Uncommon)
    {
        WithTip(typeof(MarkedCard));
    }
    public override async Task BeforeHandDraw(Player player, PlayerChoiceContext ctx, ICombatState combatState)
    {
        if (player == Owner && combatState.RoundNumber == 1)
            await CardPileCmd.AddGeneratedCardToCombat(Owner.Creature.CombatState!.CreateCard<MarkedCard>(Owner), PileType.Hand, Owner);
    }
}
