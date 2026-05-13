using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using DownfallU.DownfallUCode.Character.Snecko;
using DownfallU.DownfallUCode.Actions;

namespace DownfallU.DownfallUCode.Cards.Snecko;

// Reference: STS1 QuickBite.
public class QuickBite : SneckoCard
{
    public QuickBite() : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
    {
        WithDamage(7, 2);
        WithCards(1, 1);
        WithKeyword(SneckoKeyword.Muddle);
    }

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(ctx);
        var cards = await CardPileCmd.Draw(ctx, DynamicVars.Cards.IntValue, Owner);
        await SneckoActions.Muddle(ctx, cards);
    }
}
