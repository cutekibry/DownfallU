using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using DownfallU.DownfallUCode.Character.Snecko;

namespace DownfallU.DownfallUCode.Cards.Snecko;

[Pool(typeof(TokenCardPool))]
public class MockCharacterChoiceCard : SneckoCard
{
    public CardPoolModel? MockedPool;

    public override CardPoolModel Pool => MockedPool ?? base.Pool;

    public MockCharacterChoiceCard() : base(0, CardType.Status, CardRarity.Token, TargetType.None)
    {
        WithKeyword(SneckoKeyword.Gift);
        WithVar(new StringVar("Characters"));
    }
}
