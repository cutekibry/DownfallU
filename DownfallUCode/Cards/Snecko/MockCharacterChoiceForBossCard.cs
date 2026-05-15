using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace DownfallU.DownfallUCode.Cards.Snecko;

[Pool(typeof(TokenCardPool))]
public class MockCharacterChoiceForBossCard : SneckoCard
{
    public CardPoolModel? MockedPool;

    public override CardPoolModel Pool => MockedPool ?? base.Pool;

    public MockCharacterChoiceForBossCard() : base(0, CardType.Status, CardRarity.Token, TargetType.None)
    {
        WithVar(new StringVar("Characters"));
    }
}
