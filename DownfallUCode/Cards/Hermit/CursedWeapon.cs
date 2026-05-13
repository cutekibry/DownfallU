using System.Security.AccessControl;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Saves.Runs;
using MegaCrit.Sts2.Core.ValueProps;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class CursedWeapon : HermitCard
{
    private const int BaseDamageAmount = 10;
    private const int HpLossAmount = 2;
    private const int IncreaseAmount = 1;

    private int _currentDamage = BaseDamageAmount;
    private int _increasedDamage;

    [SavedProperty]
    public int CurrentDamage
    {
        get
        {
            return _currentDamage;
        }
        set
        {
            AssertMutable();
            _currentDamage = value;
            DynamicVars.Damage.BaseValue = _currentDamage;
        }
    }
    [SavedProperty]
    public int IncreasedDamage
    {
        get
        {
            return _increasedDamage;
        }
        set
        {
            AssertMutable();
            _increasedDamage = value;
        }
    }
    
    public CursedWeapon() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
        WithDamage(BaseDamageAmount);
        WithHpLoss(HpLossAmount);
        WithVar("Increase", IncreaseAmount);
        WithKeyword(CardKeyword.Exhaust);
        WithCostUpgradeBy(-1);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await CreatureCmd.Damage(ctx, Owner.Creature, DynamicVars.HpLoss.BaseValue, ValueProp.Unblockable | ValueProp.Unpowered, Owner.Creature, this);

        await CommonActions.CardAttack(this, play).Execute(ctx);

        BuffAllCursedWeapons();
    }

    private void BuffAllCursedWeapons()
    {
        foreach (var pileType in new[] { PileType.Hand, PileType.Draw, PileType.Discard, PileType.Exhaust })
        {
            var pile = pileType.GetPile(Owner);
            if (pile == null) continue;
            foreach (var card in pile.Cards)
            {
                if (card is CursedWeapon cw) {
                    cw.BuffFromPlay();
                    (cw.DeckVersion as CursedWeapon)?.BuffFromPlay();
                }
            }
        }
        BuffFromPlay();
        (DeckVersion as CursedWeapon)?.BuffFromPlay();
    }

    protected override void AfterDowngraded()
    {
        UpdateDamage();
    }
    private void BuffFromPlay()
    {
        IncreasedDamage += DynamicVars["Increase"].IntValue;
        UpdateDamage();
    }
    private void UpdateDamage()
    {
        CurrentDamage = BaseDamageAmount + IncreasedDamage;
    }
}
