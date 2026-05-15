using MegaCrit.Sts2.Core.Entities.Cards;

namespace DownfallU.DownfallUCode.Cards.Others;

public abstract class OthersCard(int cost, CardType type, CardRarity rarity, TargetType target) : DownfallUCard(cost, type, rarity, target)
{
    public override string CharacterId => "Others";
}