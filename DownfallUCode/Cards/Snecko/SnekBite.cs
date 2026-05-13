using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using DownfallU.DownfallUCode.Actions;

namespace DownfallU.DownfallUCode.Cards.Snecko;

public class SnekBite : SneckoCard
{
    public SnekBite() : base(1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
    {
        WithDamage(8, 1);
        WithMuddle(1, 1);
    }

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(ctx);
        await SneckoActions.MuddleHand(ctx, this);
    }
}
