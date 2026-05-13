using Godot;

namespace DownfallU.DownfallUCode.Character.Hermit;

public class HermitPotionPool : DownfallUPotionPool
{
    public override string CharacterId => Hermit.CharacterIdConst;
    public override Color Color => new(Hermit.ColorCodeConst);
}