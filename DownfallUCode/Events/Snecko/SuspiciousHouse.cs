using DownfallU.DownfallUCode.Relics.Snecko;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Acts;
using MegaCrit.Sts2.Core.Models.Encounters;
using MegaCrit.Sts2.Core.Models.Events;
using MegaCrit.Sts2.Core.Rooms;

namespace DownfallU.DownfallUCode.Events.Snecko;

public class SuspiciousHouse : SneckoEvent
{
    public override bool IsShared => true;
    public override ActModel[] Acts => [ModelDb.Act<Hive>()];
    protected override IReadOnlyList<EventOption> GenerateInitialOptions() =>
    [
        Option(Rescue, tips: HoverTipFactory.FromRelic<BabySnecko>()),
        Option(Leave)
    ];


    public Task Rescue()
    {
        var encounter = ModelDb.Encounter<OvicopterNormal>().ToMutable();
        EnterCombatWithoutExitingEvent(encounter, [], shouldResumeAfterCombat: true);
        return Task.CompletedTask;
    }
    public override async Task Resume(AbstractRoom room)
    {
        await RelicCmd.Obtain<BabySnecko>(Owner!);
        SetEventFinished(PageDescription("RESCUE"));
    }
    public Task Leave()
    {
        SetEventFinished(PageDescription("LEAVE"));
        return Task.CompletedTask;
    }
}