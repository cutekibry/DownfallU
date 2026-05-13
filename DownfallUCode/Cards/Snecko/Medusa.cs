using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using DownfallU.DownfallUCode.Powers.Snecko;
using DownfallU.DownfallUCode.Utils.Snecko;

namespace DownfallU.DownfallUCode.Cards.Snecko;

// Reference: STS1 Medusa; local CobraCoil/Belittle for GiftFilter and custom debuff power application.
public class Medusa : SneckoCard
{
    public override Func<CardModel, bool>? GiftFilter => c => GiftSelector.IsDebuff(c) && c.Rarity == CardRarity.Common;

    public Medusa() : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
    {
        WithDamage(7, 2);
        WithPower<VenomPower>(2, 1);
    }

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(ctx);
        await PowerCmd.Apply<VenomPower>(ctx, play.Target!, DynamicVars["VenomPower"].BaseValue, Owner.Creature, this);
    }
}
