using DownfallU.DownfallUCode.Powers.Hermit;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class Cheat : HermitCard
{
    public override bool HasDeadOn => true;

    public Cheat() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithCards(3, 2);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        var drawPile = PileType.Draw.GetPile(Owner);
        var topCards = drawPile.Cards.Take(DynamicVars.Cards.IntValue).ToList();
        if (topCards.Count == 0)
            return;

        var selected = (await CardSelectCmd.FromSimpleGrid(
            ctx,
            topCards,
            Owner,
            new CardSelectorPrefs(SelectionScreenPrompt, 1)
        )).FirstOrDefault();

        if (selected == null)
            return;

        if (IsDeadOn)
            await PowerCmd.Apply<CheatPower>(ctx, Owner.Creature, 1, Owner.Creature, this, true);
    
        await CardCmd.AutoPlay(ctx, selected, null);
    }
}
