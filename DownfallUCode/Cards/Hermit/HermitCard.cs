using BaseLib.Utils;
using DownfallU.DownfallUCode.Powers.Hermit;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using DownfallU.DownfallUCode.Character.Hermit;
using DownfallU.DownfallUCode.Relics.Hermit;
using DownfallU.DownfallUCode.Utils.Hermit;
using HarmonyLib;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Hooks;
using DownfallU.DownfallUCode.Extensions;

namespace DownfallU.DownfallUCode.Cards.Hermit;

[Pool(typeof(HermitCardPool))]
public abstract class HermitCard: DownfallUCard
{
    public override string CharacterId => "Hermit";

    public virtual bool HasDeadOn => false;
    private bool? _cachedIsDeadOn = null;
    protected override bool ShouldGlowGoldInternal => IsDeadOn;
    public bool IsDeadOn => _cachedIsDeadOn ?? IsDeadOnInCurrentHandState();

    public HermitCard(int cost, CardType type, CardRarity rarity, TargetType target) : base(cost, type, rarity, target)
    {
        if (HasDeadOn)
            WithKeyword(HermitKeywords.DeadOn);
    }
    public void CacheIsDeadOn()
    {
        _cachedIsDeadOn = IsDeadOnInCurrentHandState();
    }
    public void ForceSetDeadOnForPlay(bool value)
    {
        if (!HasDeadOn)
            return;
        _cachedIsDeadOn = value;
    }
    public void ClearIsDeadOnCache()
    {
        _cachedIsDeadOn = null;
    }

    private bool IsDeadOnInCurrentHandState()
    {
        if (!HasDeadOn)
            return false;

        if(((Type == CardType.Attack || Type == CardType.Skill) && Owner.Creature.HasPower<ConcentrationPower>()) || Owner.Creature.HasPower<CheatPower>())
            return true;

        var handCards = Owner.GetHand().ToList();
        int cardIndex = handCards.IndexOf(this);
        if (cardIndex == -1)
            return false;

        int handSize = handCards.Count;
        if (handSize % 2 == 0)
            return cardIndex == handSize / 2 - 1 || cardIndex == handSize / 2;
        else
            return cardIndex == handSize / 2;
    }

    protected async Task MaintanceDeadOnAfterPlayed(PlayerChoiceContext ctx, CardPlay? play)
    {
        DeadOnCounter.SetIsLastCardDeadOn(Owner, IsDeadOn);
        if (!IsDeadOn)
            return;
        DeadOnCounter.IncreaseCounter(Owner);

        var hermitPowers = Owner.Creature.Powers.OfType<HermitPower>().ToList();
        foreach (var hermitPower in hermitPowers)
            await hermitPower.AfterDeadOnTriggered(ctx, play);

        var hermitRelics = Owner.Relics.OfType<HermitRelic>().ToList();
        foreach (var hermitRelic in hermitRelics)
            await hermitRelic.AfterDeadOnTriggered(ctx, play);

    }


    protected sealed override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        bool IsSniped = Owner.Creature.HasPower<SnipePower>() && (Type == CardType.Attack || Type == CardType.Skill);
        if (HasDeadOn && IsDeadOn) {
            if (IsSniped)
                await BeforePlayInternalIfDeadOn(ctx, play);
            await BeforePlayInternalIfDeadOn(ctx, play);
        }
        await OnPlayInternal(ctx, play);
        if (HasDeadOn && IsDeadOn) {
            if (IsSniped)
                await AfterPlayInternalIfDeadOn(ctx, play);
            await AfterPlayInternalIfDeadOn(ctx, play);
        }
        await MaintanceDeadOnAfterPlayed(ctx, play);
        ClearIsDeadOnCache();
    }

    protected virtual Task BeforePlayInternalIfDeadOn(PlayerChoiceContext ctx, CardPlay play) => Task.CompletedTask;
    protected virtual Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play) => Task.CompletedTask;
    protected virtual Task AfterPlayInternalIfDeadOn(PlayerChoiceContext ctx, CardPlay play) => Task.CompletedTask;

    [HarmonyPatch(typeof(CardModel), nameof(OnPlayWrapper))]
    public static class DeadOnPlayCapturePatch
    {
        [HarmonyPrefix]
        public static void Prefix(CardModel __instance)
        {
            if (__instance is HermitCard hermitCard)
            {
                hermitCard.CacheIsDeadOn();
            }
        }
    }
    [HarmonyPatch(typeof(Hook), "BeforeSideTurnStart")]
    public static class DeadOnTurnResetPatch
    {
        [HarmonyPrefix]
        public static void Prefix()
        {
            DeadOnCounter.Reset();
        }
    }

}
