using Godot;

namespace DownfallU.DownfallUCode.Character.Snecko;

public class SneckoRelicPool : DownfallURelicPool
{
    public override string CharacterId => Snecko.CharacterIdConst;
    public override Color Color => new(Snecko.ColorCodeConst);
}