using BaseLib.Utils;
using DownfallU.DownfallUCode.Character.Hermit;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class ShortFuse : HermitCard
{

    public ShortFuse() : base(3, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
        WithDamage(18, 4);
        WithTip(HermitKeywords.Strike);
        WithTip(HermitKeywords.Defend);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(ctx);
    }

    public override Task AfterCardEnteredCombat(CardModel card)
    {
        if (card != this)
        {
            return Task.CompletedTask;
        }

        if (IsClone)
        {
            return Task.CompletedTask;
        }

        int amount = CombatManager.Instance.History.CardPlaysFinished.Count(e => (e.CardPlay.Card.Tags.Contains(CardTag.Strike) || e.CardPlay.Card.Tags.Contains(CardTag.Defend)) && e.CardPlay.Card.Owner == Owner && e.HappenedThisTurn(CombatState));
        EnergyCost.AddThisTurn(-amount);
        return Task.CompletedTask;
    }

    public override Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner != Owner)
        {
            return Task.CompletedTask;
        }

        if (!cardPlay.Card.Tags.Contains(CardTag.Strike) && !cardPlay.Card.Tags.Contains(CardTag.Defend))
        {
            return Task.CompletedTask;
        }

        EnergyCost.AddThisTurn(-1);
        return Task.CompletedTask;
    }
}
