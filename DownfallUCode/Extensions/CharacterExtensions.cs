
using DownfallU.DownfallUCode.Character.Snecko;
using MegaCrit.Sts2.Core.Models;

namespace DownfallU.DownfallUCode.Extensions;

public static class CharacterExtensions
{
    public static bool IsOffclass(this CharacterModel character)
    {
        return character is not Snecko;
    }
}