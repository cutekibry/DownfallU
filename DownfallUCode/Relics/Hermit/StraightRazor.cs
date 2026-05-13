using HarmonyLib;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Random;

namespace DownfallU.DownfallUCode.Relics.Hermit;

public class StraightRazor : HermitRelic
{
    public StraightRazor() : base(RelicRarity.Uncommon)
    {
        WithHeal(15);
        WithTip(StaticHoverTip.Transform);
    }

    public override async Task BeforeCardRemoved(CardModel card)
    {
        if(card.Owner == Owner)
        {
            await Heal();
        }
    }

    public async Task Heal()
    {
        await CreatureCmd.Heal(Owner.Creature, DynamicVars.Heal.BaseValue);
    }
    
    [HarmonyPatch(typeof(CardCmd), nameof(CardCmd.Transform), [typeof(IEnumerable<CardTransformation>), typeof(Rng), typeof(CardPreviewStyle)])]
    private static class StraightRazorTransformPatch
    {
        [HarmonyPrefix]
        public static void Prefix(ref IEnumerable<CardTransformation> transformations)
        {
            CardTransformation[] transformationArray = transformations.ToArray();
            transformations = transformationArray;

            foreach (CardTransformation transformation in transformationArray)
            {
                if (transformation.Original.Pile?.Type == PileType.Deck)
                    transformation.Original.Owner.GetRelic<StraightRazor>()?.Heal().GetAwaiter().GetResult();
            }
        }
    }

}
