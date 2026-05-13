using Godot;

namespace DownfallU.DownfallUCode.Character.Hermit;

public class HermitCardPool : DownfallUCardPool
{
    public override string CharacterId => Hermit.CharacterIdConst;

    public override float H => 0.088f;
    public override float S => 0.48f;
    public override float V => 0.47f;

    public override Color DeckEntryCardColor => new("B1814C");
}
