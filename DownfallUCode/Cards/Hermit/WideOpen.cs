using BaseLib.Utils;
using DownfallU.DownfallUCode.Actions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class WideOpen : HermitCard
{    public WideOpen() : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
    {
        WithDamage(6, 2);
        WithPower<VulnerablePower>(1, 1, true);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(ctx);
        await ShortActions.CardPower<VulnerablePower>(this, play, ctx);
    }

}
