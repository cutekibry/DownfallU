
using DownfallU.DownfallUCode.Actions;
using DownfallU.DownfallUCode.Character.Hermit;
using DownfallU.DownfallUCode.Powers.Hermit;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace DownfallU.DownfallUCode.Cards.Hermit;

public class Maintenance : HermitCard
{
    public Maintenance() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithPower<MaintenanceStrikePower>(3, 1, false);
        WithPower<DexterityPower>(1, 1, true);
        WithTip(HermitKeywords.Strike);
    }

    protected override async Task OnPlayInternal(PlayerChoiceContext ctx, CardPlay play)
    {
        await ShortActions.CardPower<MaintenanceStrikePower>(this, play, ctx);
        await ShortActions.CardPower<DexterityPower>(this, play, ctx);
        EnergyCost.AddThisCombat(1);
    }

}
