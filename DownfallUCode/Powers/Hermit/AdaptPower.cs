using DownfallU.DownfallUCode.Extensions;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DownfallU.DownfallUCode.Powers.Hermit;

public class AdaptPower : HermitPower
{
    private const int BlockPerStack = 8;

    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, ICombatState combatState)
    {
        if (side != CombatSide.Player) return;
        if (Owner?.Player == null) return;

        var hand = Owner.Player.GetHand().ToList();
        if (hand.Count == 0) return;

        // Prompt player to select a card to exhaust (optional — min 0, max 1)
        var prefs = new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, 0, 1);
        var selected = (await CardSelectCmd.FromHand(
            choiceContext,
            Owner.Player,
            prefs,
            null,
            this
        )).FirstOrDefault();

        if (selected != null)
        {
            await CardCmd.Exhaust(choiceContext, selected);
            int blockAmount = BlockPerStack * Amount;
            await CreatureCmd.GainBlock(Owner, blockAmount, default, null);
        }
    }
}
