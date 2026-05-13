using Godot;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;
using DownfallU.DownfallUCode.Cards.Snecko;
using DownfallU.DownfallUCode.Relics.Snecko;

namespace DownfallU.DownfallUCode.Character.Snecko;

public class Snecko : DownfallUCharacter
{
    public const string ColorCodeConst = "ccf2ff";
    public const string CharacterIdConst = "Snecko";

    public override string CharacterId => CharacterIdConst;
    public override Color Color => new(ColorCodeConst);
    public override Color EnergyLabelOutlineColor => new("0c3a36");
    public override CharacterGender Gender => CharacterGender.Neutral;
    public override int StartingHp => 85;

    public override int BaseOrbSlotCount => 3;

    public override IEnumerable<CardModel> StartingDeck => [
        ModelDb.Card<StrikeSnecko>(),
        ModelDb.Card<StrikeSnecko>(),
        ModelDb.Card<StrikeSnecko>(),
        ModelDb.Card<StrikeSnecko>(),
        ModelDb.Card<DefendSnecko>(),
        ModelDb.Card<DefendSnecko>(),
        ModelDb.Card<DefendSnecko>(),
        ModelDb.Card<DefendSnecko>(),
        ModelDb.Card<TailWhip>(),
        ModelDb.Card<SnekBite>(),
    ];

    public override IReadOnlyList<RelicModel> StartingRelics =>
    [
        ModelDb.Relic<SneckoSoul>()
    ];
    
    public override CardPoolModel CardPool => ModelDb.CardPool<SneckoCardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<SneckoRelicPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<SneckoPotionPool>();

    public override CreatureAnimator GenerateAnimator(MegaSprite controller)
    {
        var idle = new AnimState("Idle", isLooping: true);
        var attack = new AnimState("Attack_2");
        var cast = new AnimState("Attack");
        var hit = new AnimState("Hit");

        idle.AddBranch("Attack", attack);
        idle.AddBranch("Cast", cast);
        idle.AddBranch("Hit", hit);
        idle.AddBranch("Dead", hit);
        idle.AddBranch("Relaxed", idle);

        attack.NextState = idle;
        hit.NextState = idle;

        var animator = new CreatureAnimator(idle, controller);
        animator.AddAnyState("Idle", idle);
        animator.AddAnyState("Revive", idle);
        animator.AddAnyState("Attack", attack);
        animator.AddAnyState("Cast", cast);
        animator.AddAnyState("Hit", hit);
        animator.AddAnyState("Dead", hit);
        return animator;
    }
}
