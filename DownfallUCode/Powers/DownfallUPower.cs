
using BaseLib.Abstracts;
using BaseLib.Extensions;
using DownfallU.DownfallUCode.Extensions;

namespace DownfallU.DownfallUCode.Powers;

public abstract class DownfallUPower : CustomPowerModel
{
    public override string CustomPackedIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PowerImagePath();
    public override string CustomBigIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigPowerImagePath();
}