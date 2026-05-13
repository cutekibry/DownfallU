using Godot;
using MegaCrit.Sts2.Core.Models;

namespace DownfallU.DownfallUCode.Character.Snecko;

public class SneckoCardPool : DownfallUCardPool
{
    public override string CharacterId => Snecko.CharacterIdConst;

    public override float H => 0.55f;
    public override float S => 0.3f;
    public override float V => 1.6f;

    public override Color DeckEntryCardColor => new("85c2d6");
}