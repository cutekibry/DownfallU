using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace DownfallU.DownfallUCode.Relics.Hermit;

public class ClaspedLocket : HermitRelic
{
    private bool UsedThisTurn;
    public override bool HasUponPickupEffect => true;

    public ClaspedLocket(): base(RelicRarity.Starter)
    {
        WithCards(2);
        WithVar("Curses", 2);
        WithTip(typeof(Injury));
    }
    
    public override async Task AfterObtained()
    {
        for (int i = 0; i < DynamicVars["Curses"].BaseValue; i++)
        {
            CardModel card = Owner.RunState.CreateCard<Injury>(Owner);
            var result = await CardPileCmd.Add(card, PileType.Deck);
            CardCmd.PreviewCardPileAdd([result], 2f);
        }
    }

    public override async Task AfterCardDrawn(PlayerChoiceContext ctx, CardModel card, bool fromHandDraw)
    {
        if(card.Owner == Owner && card.Type == CardType.Curse && !UsedThisTurn)
        {
            UsedThisTurn = true;
            Flash();
            await CardCmd.Exhaust(ctx, card);
            await CardPileCmd.Draw(ctx, DynamicVars.Cards.BaseValue, Owner);
        }
    }

    public override Task AfterPlayerTurnStartEarly(PlayerChoiceContext choiceContext, Player player)
    {
        UsedThisTurn = false;
        return Task.CompletedTask;
    }

}
