using BaseLib.Utils;
using DownfallU.DownfallUCode.Utils.Hermit;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class GoldenBullet : HermitCard
{
    private int _currentCost = 3;
    [SavedProperty]
    public int CurrentCost
    {
        get
        {
            return _currentCost;
        }
        set
        {
            AssertMutable();
            _currentCost = value;
            EnergyCost.SetCustomBaseCost(_currentCost);
        }
    }
    
    public GoldenBullet() : base(3, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
        WithDamage(18, 6);
        WithKeyword(CardKeyword.Exhaust);
        WithTip(StaticHoverTip.Fatal);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        bool shouldTriggerFatal = play.Target!.Powers.All(p => p.ShouldOwnerDeathTriggerFatal());
        var attackCommand = await CommonActions.CardAttack(this, play, 0).Execute(ctx);

        if (shouldTriggerFatal && play.Target!.IsDead)
        {
            BuffFromPlay();
            // Sync to the deck version so the reduction persists after combat
            (DeckVersion as GoldenBullet)?.BuffFromPlay();
        }
    }

    protected override void AfterDowngraded()
    {
        UpdateCost();
    }
    private void BuffFromPlay()
    {
        CurrentCost = Math.Max(0, CurrentCost - 1);
        UpdateCost();
    }
    private void UpdateCost()
    {
        EnergyCost.SetCustomBaseCost(CurrentCost);
    }
}
