
using DownfallU.DownfallUCode.Character.Hermit;
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
        HermitSfx.PlaySpin();
        HermitSfx.PlayReload();

        var strikesAndDefends = PileType.Draw.GetPile(Owner).Cards
            .Where(c => c.Tags.Contains(CardTag.Strike) || c.Tags.Contains(CardTag.Defend));

        foreach (var card in strikesAndDefends)
        {
            await CardPileCmd.Add(card, PileType.Hand);
        }
    }

}
