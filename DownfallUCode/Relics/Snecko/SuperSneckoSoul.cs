using DownfallU.DownfallUCode.Actions;
using DownfallU.DownfallUCode.Cards.Snecko;
using DownfallU.DownfallUCode.Character.Snecko;
using HarmonyLib;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Relics;

namespace DownfallU.DownfallUCode.Relics.Snecko;

public class SuperSneckoSoul : SneckoCoreRelic
{
    private int RoundNumber = 0;
    public SuperSneckoSoul() : base(RelicRarity.Starter)
    {
        WithCards(1);
        WithTip(typeof(SoulRoll));
        WithTip(SneckoKeyword.Muddle);
    }

    public override async Task BeforeHandDraw(Player player, PlayerChoiceContext ctx, ICombatState combatState)
    {
        RoundNumber = combatState.RoundNumber;
        if (player == Owner && RoundNumber % 2 == 1)
        {
            Flash();
            await CardPileCmd.AddGeneratedCardToCombat(Owner.Creature.CombatState!.CreateCard<SoulRoll>(Owner), PileType.Hand, Owner);
        }
    }
    public override async Task AfterPlayerTurnStartLate(PlayerChoiceContext ctx, Player player)
    {
        if (player == Owner && RoundNumber % 2 == 0)
        {
            Flash();
            var card = await CardPileCmd.Draw(ctx, Owner);
            if (card != null)
                await SneckoActions.Muddle(ctx, card);
        }
    }
}

[HarmonyPatch(typeof(TouchOfOrobas), nameof(TouchOfOrobas.AfterObtained))]
public static class TouchOfOrobasSneckoSoulPatch
{
    public static bool Prefix(TouchOfOrobas __instance, ref Task __result)
    {
        ModelId id = __instance.StarterRelic ?? __instance.Owner.Relics.First((RelicModel r) => r.Rarity == RelicRarity.Starter).Id;
        RelicModel? relicById = __instance.Owner.GetRelicById(id);

        if (relicById is not SneckoSoul sneckoSoul)
        {
            return true;
        }

        ModelId id2 = __instance.UpgradedRelic ?? __instance.GetUpgradedStarterRelic(sneckoSoul).Id;
        RelicModel replace = ModelDb.GetById<RelicModel>(id2).ToMutable();

        if (replace is not SuperSneckoSoul superSneckoSoul)
        {
            return true;
        }

        superSneckoSoul.CharacterIds = sneckoSoul.CharacterIds.ToList();
        __result = RelicCmd.Replace(sneckoSoul, superSneckoSoul);
        return false;
    }
}
