
using BaseLib.Abstracts;
using BaseLib.Extensions;
using DownfallU.DownfallUCode.Extensions;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace DownfallU.DownfallUCode.Cards;

public abstract class DownfallUCard(int cost, CardType type, CardRarity rarity, TargetType target) : ConstructedCardModel(cost, type, rarity, target)
{
    public abstract string CharacterId { get; }
    public override string CustomPortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigCardImagePath(CharacterId);
    public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath(CharacterId);

    protected ConstructedCardModel WithPower<T>(int baseVal, int upgrade, bool hasTooltip) where T : PowerModel
    {
        WithVar(new DynamicVar(typeof(T).Name, baseVal).WithUpgrade(upgrade));
        if (hasTooltip)
            WithTip(typeof(T));
        return this;
    }
    protected ConstructedCardModel WithPower<T>(int baseVal, bool hasTooltip) where T : PowerModel
    {
        return WithPower<T>(baseVal, 0, hasTooltip);
    }

    protected ConstructedCardModel WithHpLoss(int baseVal, int upgrade = 0)
    {
        WithVar(new HpLossVar(baseVal).WithUpgrade(upgrade));
        return this;
    }
    protected ConstructedCardModel WithGold(int baseVal, int upgrade = 0)
    {
        WithVar(new GoldVar(baseVal).WithUpgrade(upgrade));
        return this;
    }
    protected ConstructedCardModel WithRepeat(int baseVal, int upgrade = 0)
    {
        WithVar(new RepeatVar(baseVal).WithUpgrade(upgrade));
        return this;
    }

    protected decimal GetCalculatedValue(string varName, CardPlay play)
    {
        return ((CalculatedVar)DynamicVars[varName]).Calculate(play.Target);
    }

}