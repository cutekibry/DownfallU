
using BaseLib.Abstracts;
using BaseLib.Extensions;
using DownfallU.DownfallUCode.Extensions;

namespace DownfallU.DownfallUCode.Powers;

public abstract class DownfallUPower : CustomPowerModel
{
    public abstract string CharacterId { get; }
    public override string CustomPackedIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PowerImagePath(CharacterId);
    public override string CustomBigIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigPowerImagePath(CharacterId);
}