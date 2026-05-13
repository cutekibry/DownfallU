using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;
using DownfallU.DownfallUCode.Cards.Snecko;

namespace DownfallU.DownfallUCode.Powers.Snecko;

public class TsunamiPower : SneckoPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterCardPlayed(PlayerChoiceContext ctx, CardPlay play)
    {
        if (play.Card.Owner != Owner.Player || play.Card is not SneckoCard sneckoCard || !sneckoCard.IsOverflowed)
            return;

        Flash();
        await CreatureCmd.GainBlock(Owner, Amount, ValueProp.Unpowered, null);
    }
}
