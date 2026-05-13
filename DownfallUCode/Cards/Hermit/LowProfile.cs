using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using BaseLib.Utils;
using DownfallU.DownfallUCode.Extensions;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class LowProfile : HermitCard
{
    public LowProfile() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
        WithCalculatedBlock(7, 4, CountDebuffs, ValueProp.Move, 2, 1);
    }

    public override bool GainsBlock => true;

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardBlock(this, DynamicVars.CalculatedBlock, play);
    }

    private static decimal CountDebuffs(CardModel card, Creature? _)
    {
        return card.Owner.Creature.Powers.Count(p => p.IsActualDebuff());
    }

}
