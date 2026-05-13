
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class Reprieve : HermitCard
{
    public Reprieve() : base(2, CardType.Skill, CardRarity.Rare, TargetType.Self)
    {
        WithHeal(10, 3);
        WithKeywords(CardKeyword.Ethereal, CardKeyword.Exhaust);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await CreatureCmd.Heal(Owner.Creature, DynamicVars.Heal.BaseValue);
    }

}
