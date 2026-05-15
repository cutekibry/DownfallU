
using System.Reflection;
using HarmonyLib;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Models;

namespace DownfallU.DownfallUCode.Relics;

[HarmonyPatch]
public static class AncientRelicAdder
{
    private static MethodInfo? _relicOptionMethod;

    public static IEnumerable<MethodBase> TargetMethods()
    {
        return AccessTools.AllTypes()
            .Where(type => !type.IsAbstract && type.IsAssignableTo(typeof(AncientEventModel)))
            .Select(type => AccessTools.PropertyGetter(type, nameof(AncientEventModel.AllPossibleOptions)))
            .Where(method => method is { IsAbstract: false })
            .Distinct()!;
    }

    public static void Postfix(AncientEventModel __instance, ref IEnumerable<EventOption> __result)
    {
        var addedOptions = new List<EventOption>();
        foreach (var relic in ModelDb.AllRelics)
        {
            if (relic is ConstructedRelicModel constructedRelic
                && constructedRelic.AncientType == __instance.GetType()
                && (constructedRelic.AncientCondition?.Invoke(__instance) ?? true)
                && TryCreateRelicOption(__instance, constructedRelic, out var option))
            {
                addedOptions.Add(option);
            }
        }

        if (addedOptions.Count > 0)
        {
            __result = (__result ?? []).Concat(addedOptions);
        }
    }

    private static bool TryCreateRelicOption(AncientEventModel eventModel, RelicModel relic, out EventOption option)
    {
        option = null!;
        var method = _relicOptionMethod ??= FindRelicOptionMethod();
        if (method == null)
        {
            return false;
        }

        var parameters = method.GetParameters();
        var mutableRelic = relic.ToMutable();
        var parameterType = parameters[0].ParameterType;
        object? argument = parameterType.IsInstanceOfType(mutableRelic)
            ? mutableRelic
            : parameterType.IsInstanceOfType(relic)
                ? relic
                : null;

        if (argument == null)
        {
            return false;
        }

        object?[] arguments = new object?[parameters.Length];
        arguments[0] = argument;
        for (int i = 1; i < parameters.Length; i++)
        {
            arguments[i] = parameters[i].DefaultValue;
        }

        option = (EventOption)method.Invoke(eventModel, arguments)!;
        return option != null;
    }

    private static MethodInfo? FindRelicOptionMethod()
    {
        for (Type? type = typeof(AncientEventModel); type != null; type = type.BaseType)
        {
            var method = AccessTools.GetDeclaredMethods(type)
                .FirstOrDefault(method =>
                    method.Name == "RelicOption"
                    && !method.ContainsGenericParameters
                    && typeof(EventOption).IsAssignableFrom(method.ReturnType)
                    && IsRelicOptionMethod(method));

            if (method != null)
            {
                return method;
            }
        }

        return null;
    }

    private static bool IsRelicOptionMethod(MethodInfo method)
    {
        var parameters = method.GetParameters();
        return parameters.Length > 0
            && typeof(RelicModel).IsAssignableFrom(parameters[0].ParameterType)
            && parameters.Skip(1).All(parameter => parameter.IsOptional);
    }
}
