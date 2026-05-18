using BaseLib.Utils;
using DownfallU.DownfallUCode.Actions;
using DownfallU.DownfallUCode.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.PotionPools;
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace DownfallU.DownfallUCode.Potions.Snecko;

[Pool(typeof(SharedPotionPool))]
public class MuddlingPotion : SneckoPotion
{
    public override PotionRarity Rarity => PotionRarity.Common;
    public override PotionUsage Usage => PotionUsage.CombatOnly;
    public override TargetType TargetType => TargetType.AnyPlayer;

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new EnergyVar(3),
        new CardsVar(2)
    ];

    public override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.ForEnergy(this)
    ];

    protected override async Task OnUse(PlayerChoiceContext ctx, Creature? target)
    {
        AssertValidForTargetedPotion(target);
        NCombatRoom.Instance?.PlaySplashVfx(target, new Color("5f37ff"));
        
        var hand = target.Player!.GetHand().Where(c => c.CanBeMuddled()).ToList();
        for (int i = 0; i < DynamicVars.Cards.IntValue && hand.Count != 0; i++) {
            var highestCost = hand.Max(c => c.EnergyCost.GetResolved());
            var selected = hand.Where(c => c.EnergyCost.GetResolved() == highestCost).CombatRandomTake(1, target.Player!).First();
            hand.Remove(selected);
            await SneckoActions.Muddle(ctx, selected, maxCost: 2);
        }
    }
}