using BaseLib.Abstracts;
using DownfallU.DownfallUCode.Extensions;
using Godot;

namespace DownfallU.DownfallUCode.Character.Snecko;

public class SneckoPotionPool : CustomPotionPoolModel
{
    public override Color LabOutlineColor => Snecko.Color;
    

    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}