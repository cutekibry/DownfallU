using DownfallU.DownfallUCode.Character.Snecko;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace DownfallU.DownfallUCode.Relics.Snecko;

public class ConfusingCodex : SneckoRelic
{
    public override bool ShowCounter => true;
    private int _counter = 0;

    [SavedProperty]
    public int Counter
    {
        get
        {
            return _counter;
        }
        set
        {
            AssertMutable();
            _counter = value;
            InvokeDisplayAmountChanged();
        }
    }
    public override int DisplayAmount => Counter;

    public ConfusingCodex() : base(RelicRarity.Rare)
    {
        WithVar("Times", 3);
        WithPower<WeakPower>(1, true);
        WithPower<VulnerablePower>(1, true);
        WithTip(SneckoKeyword.Overflow);
    }

    public override async Task AfterCardOverflowed(PlayerChoiceContext ctx, CardModel card)
    {
        if (card.Owner != Owner)
            return;

        Counter = (Counter + 1) % DynamicVars["Times"].IntValue;
        Status = (Counter == DynamicVars["Times"].IntValue - 1) ? RelicStatus.Active : RelicStatus.Normal;
        if (Counter == 0)
        {
            Flash();
            foreach (var enemy in card.CombatState!.HittableEnemies)
            {
                await PowerCmd.Apply<WeakPower>(ctx, enemy, DynamicVars["WeakPower"].BaseValue, Owner.Creature, null);
                await PowerCmd.Apply<VulnerablePower>(ctx, enemy, DynamicVars["VulnerablePower"].BaseValue, Owner.Creature, null);
            }
        }
    }

}
