using BaseLib.Abstracts;
using DownfallU.DownfallUCode.Extensions;
using Godot;

namespace DownfallU.DownfallUCode.Character;

public abstract class DownfallUPotionPool : CustomPotionPoolModel
    {
    public abstract string CharacterId { get; }
    public abstract Color Color { get; }

    public override Color LabOutlineColor => Color;

    public override string BigEnergyIconPath => "big_energy.png".CharacterUiPath(CharacterId);
    public override string TextEnergyIconPath => "text_energy.png".CharacterUiPath(CharacterId);
}