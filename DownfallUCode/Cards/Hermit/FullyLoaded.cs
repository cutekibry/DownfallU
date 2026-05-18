
using DownfallU.DownfallUCode.Character.Hermit;
using DownfallU.DownfallUCode.Extensions;
using DownfallU.DownfallUCode.Utils.Hermit;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class FullyLoaded : HermitCard
{
    public FullyLoaded() : base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithKeyword(CardKeyword.Exhaust);
        WithTip(HermitKeywords.Strike);
        WithTip(HermitKeywords.Defend);
        WithKeyword(CardKeyword.Retain, UpgradeType.Add);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        var strikesAndDefends = Owner.GetDraw()
            .Where(c => c.Tags.Contains(CardTag.Strike) || c.Tags.Contains(CardTag.Defend)).ToList();
        foreach (var card in strikesAndDefends)
        {
            await CardPileCmd.Add(card, PileType.Hand);
        }
    }

}
