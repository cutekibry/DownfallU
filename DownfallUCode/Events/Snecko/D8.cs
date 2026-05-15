using System.Linq.Expressions;
using BaseLib.Abstracts;
using DownfallU.DownfallUCode.Cards.Others;
using DownfallU.DownfallUCode.Cards.Snecko;
using DownfallU.DownfallUCode.Character.Snecko;
using DownfallU.DownfallUCode.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Events;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.ValueProps;

using D8Relic = DownfallU.DownfallUCode.Relics.Snecko.D8;

namespace DownfallU.DownfallUCode.Events.Snecko;

public class D8 : SneckoEvent
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new HpLossVar(10)
    ];
    protected override IReadOnlyList<EventOption> GenerateInitialOptions() =>
    [
        Option(Break, tips: HoverTipFactory.FromKeyword(SneckoKeyword.Offclass)).ThatDoesDamage(DynamicVars.HpLoss.IntValue),
        (
            Owner!.GetDeck().Any(c => c is SneckoCard { HasOverflow: true, Enchantment: null })
            ?
            Option(Take, tips: [..HoverTipFactory.FromRelic<D8Relic>(), HoverTipFactory.FromCard<Pain>()])
            :
            OptionLocked(Take, tips: [HoverTipFactory.FromKeyword(SneckoKeyword.Overflow)])
        ),
        Option(Leave)
    ];


    public async Task Break()
    {
        await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), Owner!.Creature, DynamicVars.HpLoss.IntValue, ValueProp.Unblockable | ValueProp.Unpowered, null, null);

        var pools = Owner.GetOffclassCardPoolsForGift();
        var options = CardCreationOptions.ForNonCombatWithDefaultOdds(pools, c => c.Rarity == CardRarity.Rare).WithFlags(CardCreationFlags.NoRarityModification | CardCreationFlags.NoCardPoolModifications);
        await RewardsCmd.OfferCustom(Owner, [new CardReward(options, 3, Owner)]);
        SetEventFinished(PageDescription("BREAK"));
    }
    public async Task Take()
    {
        await RelicCmd.Obtain<D8Relic>(Owner!);
        await CardPileCmd.AddCurseToDeck<Pain>(Owner!);
        SetEventFinished(PageDescription("TAKE"));
    }
    public Task Leave()
    {
        SetEventFinished(PageDescription("LEAVE"));
        return Task.CompletedTask;
    }
}