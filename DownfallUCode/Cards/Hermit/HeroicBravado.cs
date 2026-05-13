
using DownfallU.DownfallUCode.Actions;
using DownfallU.DownfallUCode.Powers.Hermit;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class HeroicBravado : HermitCard
{
    public HeroicBravado() : base(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
    {
        WithKeyword(CardKeyword.Ethereal);
        WithEnergy(2, -1);
        WithPower<RuggedPower>(1);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        EnergyCost.AddThisCombat(1);
        await ShortActions.CardPower<RuggedPower>(this, play, ctx);
    }

}
