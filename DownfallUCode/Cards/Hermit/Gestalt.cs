using DownfallU.DownfallUCode.Actions;
using DownfallU.DownfallUCode.Powers.Hermit;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

using MegaCrit.Sts2.Core.Models.Powers;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class Gestalt : HermitCard
{

    public Gestalt() : base(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithPower<RuggedPower>(2, true);
        WithPower<VulnerablePower>(2, -1, true);
        WithKeyword(CardKeyword.Exhaust);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await ShortActions.CardPower<RuggedPower>(this, play, ctx);
        await ShortActions.CardPower<VulnerablePower>(this, play, ctx);
    }

}
