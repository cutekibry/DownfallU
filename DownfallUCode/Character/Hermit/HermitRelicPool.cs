using Godot;

namespace DownfallU.DownfallUCode.Character.Hermit;

public class HermitRelicPool : DownfallURelicPool
{
    public override string CharacterId => Hermit.CharacterIdConst;
    public override Color Color => new(Hermit.ColorCodeConst);
}