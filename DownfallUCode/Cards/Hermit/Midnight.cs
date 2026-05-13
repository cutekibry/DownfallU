
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class Midnight : HermitCard
{
    public Midnight() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithBlock(12, 3);
        WithTip(typeof(ImpendingDoom));
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardBlock(this, play);
        var doom = CombatState!.CreateCard<ImpendingDoom>(Owner);
        await CardPileCmd.AddGeneratedCardToCombat(doom, PileType.Hand, Owner);
    }

}
