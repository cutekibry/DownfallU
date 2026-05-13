using BaseLib.Extensions;
using DownfallU.DownfallUCode.Extensions;
using MegaCrit.Sts2.Core.Entities.Relics;

namespace DownfallU.DownfallUCode.Relics;

public abstract class DownfallURelic(RelicRarity rarity) : ConstructedRelicModel(rarity)
{
    public abstract string CharacterID { get; }
    public override string PackedIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".RelicImagePath(CharacterID);
    protected override string PackedIconOutlinePath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}_outline.png".RelicImagePath(CharacterID);
    protected override string BigIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigRelicImagePath(CharacterID);
}