using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using DownfallU.DownfallUCode.Character.Snecko;
using DownfallU.DownfallUCode.Extensions;

namespace DownfallU.DownfallUCode.Cards.Snecko;

// Reference: STS1 CrystalBoomerang; STS2 Hologram/Dredge for discard-pile selection into hand.
public class CrystalBoomerang : SneckoCard
{
    public CrystalBoomerang() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
        WithBlock(5, 3);
        WithKeyword(SneckoKeyword.Offclass);
    }

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        var selected = (await CardSelectCmd.FromSimpleGrid(ctx, PileType.Discard.GetPile(Owner).Cards, Owner, new CardSelectorPrefs(SelectionScreenPrompt, 1))).FirstOrDefault();
        if (selected == null)
            return;

        bool offclass = selected.IsOffclass();
        await CardPileCmd.Add(selected, PileType.Hand);
        if (offclass)
            await CommonActions.CardBlock(this, play);
    }
}
