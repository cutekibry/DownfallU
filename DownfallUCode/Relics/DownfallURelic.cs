using BaseLib.Abstracts;
using BaseLib.Extensions;
using DownfallU.DownfallUCode.Extensions;

namespace DownfallU.DownfallUCode.Relics;

public abstract class DownfallURelic : CustomRelicModel
{
    public override string PackedIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".RelicImagePath();
    protected override string PackedIconOutlinePath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}_outline.png".RelicImagePath();
    protected override string BigIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigRelicImagePath();
}