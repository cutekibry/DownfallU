using Godot;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;
using DownfallU.DownfallUCode.Cards.Hermit;
using DownfallU.DownfallUCode.Relics.Hermit;

namespace DownfallU.DownfallUCode.Character.Hermit;

public class Hermit : DownfallUCharacter
{
    public const string ColorCodeConst = "9e6a34";
    public const string CharacterIdConst = "Hermit";

    public override string CharacterId => CharacterIdConst;
    public override Color Color => new(ColorCodeConst);

    public override CharacterGender Gender => CharacterGender.Neutral;
    public override int StartingHp => 70;
    public override int StartingGold => 99;

    public override IEnumerable<CardModel> StartingDeck =>
    [
        ModelDb.Card<StrikeHermit>(),
        ModelDb.Card<StrikeHermit>(),
        ModelDb.Card<StrikeHermit>(),
        ModelDb.Card<StrikeHermit>(),
        ModelDb.Card<DefendHermit>(),
        ModelDb.Card<DefendHermit>(),
        ModelDb.Card<DefendHermit>(),
        ModelDb.Card<DefendHermit>(),
        ModelDb.Card<Covet>(),
        ModelDb.Card<Snapshot>()
    ];

    public override IReadOnlyList<RelicModel> StartingRelics =>
    [
        ModelDb.Relic<OldLocket>()
    ];

    public override CardPoolModel CardPool => ModelDb.CardPool<HermitCardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<HermitRelicPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<HermitPotionPool>();

    public override CreatureAnimator GenerateAnimator(MegaSprite controller)
    {
        var idle = new AnimState("Idle", isLooping: true);
        var attack = new AnimState("HitRugged");
        var hit = new AnimState("Hit");

        idle.AddBranch("Attack", attack);
        idle.AddBranch("Cast", attack);
        idle.AddBranch("Hit", hit);
        idle.AddBranch("Dead", hit);
        idle.AddBranch("Relaxed", idle);

        attack.NextState = idle;
        hit.NextState = idle;

        var animator = new CreatureAnimator(idle, controller);
        animator.AddAnyState("Idle", idle);
        animator.AddAnyState("Revive", idle);
        animator.AddAnyState("Attack", attack);
        animator.AddAnyState("Cast", attack);
        animator.AddAnyState("Hit", hit);
        animator.AddAnyState("Dead", hit);
        return animator;
    }
}
