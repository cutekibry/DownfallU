



using BaseLib.Utils;
using DownfallU.DownfallUCode.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.ValueProps;

namespace DownfallU.DownfallUCode.Cards.Others;

[Pool(typeof(CurseCardPool))]
public class Pain : OthersCard
{
    public override int MaxUpgradeLevel => 0;
    public Pain() : base(-1, CardType.Curse, CardRarity.Curse, TargetType.None)
    {
        WithKeyword(CardKeyword.Unplayable);
        WithHpLoss(1);
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext ctx, CardPlay play)
    {
        if (play.Card.Owner == Owner && Owner.GetHand().Contains(this))
        {
            await CreatureCmd.Damage(ctx, Owner.Creature, DynamicVars.HpLoss.IntValue, ValueProp.Unblockable | ValueProp.Unpowered, Owner.Creature);
        }
    }
}