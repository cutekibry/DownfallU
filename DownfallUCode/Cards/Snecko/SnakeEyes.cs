using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using DownfallU.DownfallUCode.Powers.Snecko;
using DownfallU.DownfallUCode.Character.Snecko;

namespace DownfallU.DownfallUCode.Cards.Snecko;

// Reference: STS1 SnakeEyes; STS2 BurstPower for replaying the next matching card.
public class SnakeEyes : SneckoCard
{
    public override Func<CardModel, bool>? GiftFilter => c => c.Type == CardType.Skill && c.Rarity == CardRarity.Rare;

    public SnakeEyes() : base(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
    {
        WithPower<SnakeEyesPower>(1, 1, false);
        WithKeyword(SneckoKeyword.Offclass);
    }

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await PowerCmd.Apply<SnakeEyesPower>(ctx, Owner.Creature, DynamicVars["SnakeEyesPower"].BaseValue, Owner.Creature, this);
    }
}
