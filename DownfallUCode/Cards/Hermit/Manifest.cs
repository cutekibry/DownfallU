
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Cards;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class Manifest : HermitCard
{
    public Manifest() : base(2, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
        WithBlock(16, 4);
        WithTip(typeof(Decay));
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardBlock(this, play);
        var decay = CombatState!.CreateCard<Decay>(Owner);
        await CardPileCmd.AddGeneratedCardToCombat(decay, PileType.Hand, Owner);
    }

}
