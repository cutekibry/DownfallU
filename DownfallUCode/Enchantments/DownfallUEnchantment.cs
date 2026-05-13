
using BaseLib.Abstracts;
using BaseLib.Extensions;
using DownfallU.DownfallUCode.Extensions;

namespace DownfallU.DownfallUCode.Enchantments;

public abstract class DownfallUEnchantment : CustomEnchantmentModel
{
    protected override string CustomIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".EnchantmentImagePath();
}