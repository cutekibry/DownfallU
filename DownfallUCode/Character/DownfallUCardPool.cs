
using BaseLib.Abstracts;
using DownfallU.DownfallUCode.Extensions;

namespace DownfallU.DownfallUCode.Character;

public abstract class DownfallUCardPool : CustomCardPoolModel
{
    public abstract string CharacterId { get; }
    public override string Title => CharacterId; //This is not a display name.

    public override string BigEnergyIconPath => "big_energy.png".CharacterUiPath(CharacterId);
    public override string TextEnergyIconPath => "text_energy.png".CharacterUiPath(CharacterId);

    public override bool IsColorless => false;
}