using Godot;

namespace DownfallU.DownfallUCode.Character.Snecko;

public class SneckoPotionPool : DownfallUPotionPool
{
    public override string CharacterId => Snecko.CharacterIdConst;
    public override Color Color => new(Snecko.ColorCodeConst);
}