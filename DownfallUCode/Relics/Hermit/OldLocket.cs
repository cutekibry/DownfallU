using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Rooms;
using DownfallU.DownfallUCode.Cards.Hermit;

namespace DownfallU.DownfallUCode.Relics.Hermit;

public class OldLocket : HermitRelic
{
    private bool _firstTurn = true;

    public OldLocket() : base(RelicRarity.Starter)
    {
        WithTip(typeof(MementoCard));
    }

    public override RelicModel? GetUpgradeReplacement()
    {
        return ModelDb.Relic<ClaspedLocket>();
    }

    public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, ICombatState combatState)
    {
        if (!_firstTurn || side != Owner.Creature.Side) return;
        _firstTurn = false;

        Flash();
        var card = combatState.CreateCard<MementoCard>(Owner);
        await CardPileCmd.AddGeneratedCardToCombat(
            card,
            PileType.Hand,
            Owner
        );
    }

    public override Task AfterCombatEnd(CombatRoom _)
    {
        _firstTurn = true;
        return Task.CompletedTask;
    }
}
