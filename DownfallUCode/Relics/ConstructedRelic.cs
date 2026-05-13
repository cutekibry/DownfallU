using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace DownfallU.DownfallUCode.Relics;

public abstract class ConstructedRelicModel(RelicRarity rarity) : CustomRelicModel()
{
    private readonly RelicRarity _rarity = rarity;
    private readonly List<DynamicVar> _constructedDynamicVars = [];

    private readonly List<TooltipSource> _hoverTips = [];

    public sealed override RelicRarity Rarity => _rarity;
    protected sealed override IEnumerable<DynamicVar> CanonicalVars => _constructedDynamicVars;
    protected sealed override IEnumerable<IHoverTip> ExtraHoverTips => _hoverTips.Select(t => t.Tip(null!));

    protected ConstructedRelicModel WithVars(params DynamicVar[] vars)
    {
        foreach (DynamicVar dynamicVar in vars)
        {
            _constructedDynamicVars.Add(dynamicVar);
            Type type = dynamicVar.GetType();
            if (!type.IsGenericType)
            {
                continue;
            }

            Type[] genericArguments = type.GetGenericArguments();
            foreach (Type type2 in genericArguments)
            {
                if (type2.IsAssignableTo(typeof(PowerModel)))
                {
                    WithTip(type2);
                }
            }
        }

        return this;
    }

    protected ConstructedRelicModel WithVar(string name, int baseVal)
    {
        _constructedDynamicVars.Add(new DynamicVar(name, baseVal));
        return this;
    }

    protected ConstructedRelicModel WithVar(DynamicVar var)
    {
        return WithVars(var);
    }

    protected ConstructedRelicModel WithBlock(int baseVal)
    {
        _constructedDynamicVars.Add(new BlockVar(baseVal, ValueProp.Unpowered));
        return this;
    }

    protected ConstructedRelicModel WithDamage(int baseVal)
    {
        _constructedDynamicVars.Add(new DamageVar(baseVal, ValueProp.Unpowered));
        return this;
    }

    protected ConstructedRelicModel WithCards(int baseVal)
    {
        CardsVar item = new(baseVal);
        _constructedDynamicVars.Add(item);
        return this;
    }

    protected ConstructedRelicModel WithEnergy(int baseVal)
    {
        EnergyVar item = new(baseVal);
        _constructedDynamicVars.Add(item);
        WithTip(new TooltipSource(HoverTipFactory.ForEnergy));
        return this;
    }

    protected ConstructedRelicModel WithHeal(int baseVal)
    {
        HealVar item = new(baseVal);
        _constructedDynamicVars.Add(item);
        return this;
    }

    protected ConstructedRelicModel WithPower<T>(int baseVal, bool addTooltip) where T : PowerModel
    {
        _constructedDynamicVars.Add(new PowerVar<T>(baseVal));
        if (addTooltip)
        {
            _hoverTips.Add(new TooltipSource(_ => HoverTipFactory.FromPower<T>()));
        }
        return this;
    }

    protected ConstructedRelicModel WithPower<T>(string name, int baseVal, bool addTooltip) where T : PowerModel
    {
        _constructedDynamicVars.Add(new PowerVar<T>(name, baseVal));
        if (addTooltip)
        {
            _hoverTips.Add(new TooltipSource(_ => HoverTipFactory.FromPower<T>()));
        }
        return this;
    }

    protected ConstructedRelicModel WithTip(TooltipSource tipSource)
    {
        _hoverTips.Add(tipSource);
        return this;
    }

    protected ConstructedRelicModel WithGold(int baseVal)
    {
        GoldVar item = new(baseVal);
        _constructedDynamicVars.Add(item);
        return this;
    }

}