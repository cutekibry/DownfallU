using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using DownfallU.DownfallUCode.Character.Snecko;
using DownfallU.DownfallUCode.Extensions;

namespace DownfallU.DownfallUCode.Cards.Snecko;


public class BeyondArmor : SneckoCard
{
    public override Func<CardModel, bool>? GiftFilter => c => c.Rarity == CardRarity.Common;
    public BeyondArmor() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
        WithBlock(5, 3);
        WithCards(2);
        WithKeyword(SneckoKeyword.Offclass);
    }

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardBlock(this, play); 
        var cards = PileType.Draw.GetPile(Owner).Cards.Where(c => c.IsOffclass()).ToList().StableShuffle(Owner.RunState.Rng.Shuffle).Take(DynamicVars.Cards.IntValue);
        await CardPileCmd.Add(cards, PileType.Hand);
    }
}