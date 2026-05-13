using DownfallU.DownfallUCode.Actions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class Scavenge : HermitCard
{
    public override bool HasDeadOn => true;

    public Scavenge() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithPower<PlatingPower>(4, 1, true);
        WithGold(5, 5);
        WithKeyword(CardKeyword.Exhaust);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await ShortActions.CardPower<PlatingPower>(this, play, ctx);
    }

    protected override async Task AfterPlayInternalIfDeadOn(PlayerChoiceContext ctx, CardPlay play)
    {
        await PlayerCmd.GainGold(DynamicVars.Gold.BaseValue, Owner);
    }

}
