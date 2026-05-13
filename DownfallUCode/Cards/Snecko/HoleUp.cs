using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;

namespace DownfallU.DownfallUCode.Cards.Snecko;

// Reference: HermitMod HoleUp for values/text; local DragonsHoard and STS2 Doubt for self-applied powers.
[Pool(typeof(TokenCardPool))]
public class HoleUp : SneckoCard
{
    public HoleUp() : base(1, CardType.Skill, CardRarity.Token, TargetType.Self)
    {
        WithBlock(12, 4);
        WithPower<WeakPower>(2);
    }

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardBlock(this, play);
        await PowerCmd.Apply<WeakPower>(ctx, Owner.Creature, DynamicVars["WeakPower"].BaseValue, Owner.Creature, this);
    }
}
