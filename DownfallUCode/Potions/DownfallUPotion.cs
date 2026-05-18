using BaseLib.Abstracts;
using BaseLib.Extensions;
using DownfallU.DownfallUCode.Extensions;

namespace DownfallU.DownfallUCode.Potions;
public abstract class DownfallUPotion : CustomPotionModel
{
    public abstract string CharacterId { get; }

    public override string? CustomPackedImagePath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PotionImagePath(CharacterId);
}