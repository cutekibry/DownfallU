using DownfallU.DownfallUCode.Actions;
using DownfallU.DownfallUCode.Powers.Hermit;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class ShadowCloak : HermitCard
{
    public ShadowCloak() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self)
    {
        WithPower<ShadowCloakPower>(4, 2, false);
        WithTip(CardKeyword.Exhaust);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await ShortActions.CardPower<ShadowCloakPower>(this, play, ctx);
    }
}
