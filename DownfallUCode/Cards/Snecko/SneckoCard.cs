using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using System.Reflection;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using HarmonyLib;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using DownfallU.DownfallUCode.Character.Snecko;
using DownfallU.DownfallUCode.Extensions;
using DownfallU.DownfallUCode.Hooks;
using DownfallU.DownfallUCode.Enchantments;

namespace DownfallU.DownfallUCode.Cards.Snecko;

[Pool(typeof(SneckoCardPool))]
public abstract class SneckoCard : DownfallUCard
{
    public override string CharacterId => "Snecko";

    public virtual bool HasOverflow => false;
    private bool? _cachedIsOverflowed = null;
    protected override bool ShouldGlowGoldInternal => IsOverflowed;
    public bool IsOverflowed => _cachedIsOverflowed ?? IsCurrentlyOverflowed;
    private bool IsCurrentlyOverflowed => HasOverflow && (Enchantment is Lead || Owner.GetHand().Count > 5);

    public SneckoCard(int cost, CardType type, CardRarity rarity, TargetType target) : base(cost, type, rarity, target)
    {
        if (HasOverflow)
            WithKeyword(SneckoKeyword.Overflow);
        if (GiftFilter != null)
            WithKeyword(SneckoKeyword.Gift);
    }

    public void CacheIsOverflowed()
    {
        _cachedIsOverflowed = IsCurrentlyOverflowed;
    }
    public void ClearIsOverflowedCache()
    {
        _cachedIsOverflowed = null;
    }

    public virtual Func<CardModel, bool>? GiftFilter => null;

    public virtual Task AfterMuddled(PlayerChoiceContext choiceContext) => Task.CompletedTask;

    protected ConstructedCardModel WithMuddle(int baseVal, int upgrade = 0)
    {
        WithKeyword(SneckoKeyword.Muddle);
        WithVar(new DynamicVar("Muddle", baseVal).WithUpgrade(upgrade));
        return this;
    }

    [HarmonyPatch(typeof(CardModel), nameof(OnPlayWrapper))]
    public static class OverflowCapturePatch
    {
        [HarmonyPrefix]
        public static void Prefix(CardModel __instance)
        {
            if (__instance is SneckoCard sneckoCard)
                sneckoCard.CacheIsOverflowed();
        }

        [HarmonyPostfix]
        public static void Postfix(CardModel __instance, ref Task __result)
        {
            if (__instance is SneckoCard)
                __result = ClearOverflowCaptureAfterPlay(__instance, __result);
        }

        private static async Task ClearOverflowCaptureAfterPlay(CardModel card, Task original)
        {
            try
            {
                await original;
            }
            finally
            {
                if (card is SneckoCard sneckoCard)
                {
                    sneckoCard.ClearIsOverflowedCache();
                }
            }
        }
    }

    [HarmonyPatch]
    public static class OverflowPlayContextPatch
    {
        public static IEnumerable<MethodBase> TargetMethods()
        {
            return AccessTools.AllTypes()
                .Where(type => !type.IsAbstract && type.IsAssignableTo(typeof(SneckoCard)))
                .Select(type => AccessTools.DeclaredMethod(type, nameof(OnPlay), [typeof(PlayerChoiceContext), typeof(CardPlay)]))
                .Where(method => method is { IsAbstract: false })
                .Distinct()!;
        }

        [HarmonyPostfix]
        public static void Postfix(SneckoCard __instance, PlayerChoiceContext ctx, ref Task __result)
        {
            __result = TriggerOverflowHookAfterPlay(__instance, ctx, __result);
        }

        private static async Task TriggerOverflowHookAfterPlay(SneckoCard card, PlayerChoiceContext ctx, Task original)
        {
            await original;
            if (card.IsOverflowed)
            {
                await SneckoHooks.TriggerAfterCardOverflowed(ctx, card);
            }
        }
    }
}
