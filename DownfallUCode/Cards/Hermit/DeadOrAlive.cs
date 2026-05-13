using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.HoverTips;
using DownfallU.DownfallUCode.Character.Hermit;
using BaseLib.Utils;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class DeadOrAlive : HermitCard
{
    private const int MonsterGoldAmount = 15;
    private const int EliteGoldAmount = 40;
    private const int BossGoldAmount = 100;

    public DeadOrAlive() : base(1, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
    {
        WithDamage(8, 3);
        WithKeyword(CardKeyword.Exhaust);
        WithTip(StaticHoverTip.Fatal);
        WithKeyword(HermitKeywords.Bounty);
    }

    protected override bool HasEnergyCostX => true;

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        int times = EnergyCost.CapturedXValue;

        await CommonActions.CardAttack(this, play, EnergyCost.GetResolved()).Execute(ctx);

        bool shouldTriggerFatal = play.Target!.Powers.All(p => p.ShouldOwnerDeathTriggerFatal());
        if (play.Target.IsDead && shouldTriggerFatal)
        {
            AbstractRoom? currentRoom = Owner.Creature.CombatState?.RunState.CurrentRoom;

            ArgumentNullException.ThrowIfNull(currentRoom);
            var goldAmount = currentRoom.RoomType switch
            {
                RoomType.Monster => MonsterGoldAmount,
                RoomType.Elite => EliteGoldAmount,
                RoomType.Boss => BossGoldAmount,
                _ => throw new InvalidOperationException("Invalid room type for Dead Or Alive card."),
            };
            await PlayerCmd.GainGold(goldAmount, Owner);
        }
    }

}
