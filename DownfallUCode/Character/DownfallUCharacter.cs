using BaseLib.Abstracts;
using BaseLib.Utils.NodeFactories;
using DownfallU.DownfallUCode.Extensions;
using Godot;

namespace DownfallU.DownfallUCode.Character;

public abstract class DownfallUCharacter : PlaceholderCharacterModel
{
    public abstract string CharacterId { get; }
    public abstract Color Color { get; }

    public override Color NameColor => Color;
    public override Color MapDrawingColor => Color;

    public override Color RemoteTargetingLineColor => Color;

    public override Color RemoteTargetingLineOutline => Color;

    /*  PlaceholderCharacterModel will utilize placeholder basegame assets for most of your character assets until you
        override all the other methods that define those assets. 
        These are just some of the simplest assets, given some placeholders to differentiate your character with. 
        You don't have to, but you're suggested to rename these images. */
    public override Control CustomIcon
    {
        get
        {
            var icon = NodeFactory<Control>.CreateFromResource(CustomIconTexturePath!);
            icon.SetAnchorsAndOffsetsPreset(Control.LayoutPreset.FullRect);
            return icon;
        }
    }
    public override string CustomCharacterSelectBg => $"res://DownfallU/scenes/screens/char_select/char_select_bg_{CharacterId.ToLowerInvariant()}.tscn";
    public override string CustomEnergyCounterPath => $"res://DownfallU/scenes/combat/energy_counters/{CharacterId.ToLowerInvariant()}_energy_counter.tscn";
    public override string CustomVisualPath => $"res://DownfallU/scenes/creature_visuals/{CharacterId.ToLowerInvariant()}.tscn";

    public override string CustomIconTexturePath => $"character_icon_{CharacterId.ToLowerInvariant()}.png".CharacterUiPath(CharacterId);
    public override string CustomIconOutlineTexturePath => $"character_icon_{CharacterId.ToLowerInvariant()}_outline.png".CharacterUiPath(CharacterId);
    public override string CustomCharacterSelectIconPath => $"char_select_{CharacterId.ToLowerInvariant()}.png".CharacterUiPath(CharacterId);
    public override string CustomCharacterSelectLockedIconPath => $"char_select_{CharacterId.ToLowerInvariant()}_locked.png".CharacterUiPath(CharacterId);
    public override string CustomMapMarkerPath => $"map_marker_{CharacterId.ToLowerInvariant()}.png".CharacterUiPath(CharacterId);
}