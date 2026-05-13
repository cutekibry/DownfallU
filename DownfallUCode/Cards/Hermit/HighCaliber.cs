
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Enchantments;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class HighCaliber : HermitCard
{
    public HighCaliber() : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
    {
        WithDamage(6, 3);
        WithVar("SharpAmount", 6);
        WithKeyword(CardKeyword.Exhaust);
        WithUpgradingCardTip<StrikeHermit>();
        WithTip(typeof(Sharp));
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(ctx);

        CardModel strike = CombatState!.CreateCard<StrikeHermit>(Owner);
        CardCmd.Enchant<Sharp>(strike, DynamicVars["SharpAmount"].BaseValue);
        if(IsUpgraded)
            CardCmd.Upgrade(strike);
        await CardPileCmd.AddGeneratedCardToCombat(strike, PileType.Hand, Owner);
    }

}
