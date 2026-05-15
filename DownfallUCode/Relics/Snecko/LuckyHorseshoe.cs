using DownfallU.DownfallUCode.Cards.Snecko;
using DownfallU.DownfallUCode.Character.Snecko;
using DownfallU.DownfallUCode.Extensions;
using DownfallU.DownfallUCode.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Events;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Saves.Runs;
using SneckoCharacter = DownfallU.DownfallUCode.Character.Snecko.Snecko;

namespace DownfallU.DownfallUCode.Relics.Snecko;

public class LuckyHorseshoe : SneckoRelic
{
    public override bool HasUponPickupEffect => true;

    private ModelId? _characterId;
    [SavedProperty]
    public ModelId? CharacterId
    {
        get
        {
            return _characterId;
        }
        set
        {
            AssertMutable();
            _characterId = value;
            ((StringVar)DynamicVars["Character"]).StringValue = Character?.Title.GetFormattedText() ?? "";
        }
    }
    private CharacterModel? Character
    {
        get
        {
            if (!(CharacterId != null))
            {
                return null;
            }

            return ModelDb.GetById<CharacterModel>(CharacterId);
        }
    }

    public LuckyHorseshoe() : base(RelicRarity.Ancient)
    {
        WithAncient<Nonupeipe, SneckoCharacter>();
        WithTip(SneckoKeyword.Offclass);
        WithVar(new StringVar("Character"));
        WithVar("CharacterCount", 3);
        WithVar("UncommonCount", 3);
        WithVar("RareCount", 2);
    }

    public override async Task AfterObtained()
    {
        var characters = Owner.UnlockState.Characters.Where(c => c.IsOffclass()).ToList();

        var rng = Owner.PlayerRng.Rewards;

        var firstCharacter = rng.NextItem(characters)!;
        var secondCharacter = rng.NextItem(characters.Where(c => c.Id != firstCharacter.Id))!;
        var thirdCharacter = rng.NextItem(characters.Where(c => c.Id != firstCharacter.Id && c.Id != secondCharacter.Id))!;

        var firstCard = Owner.RunState.CreateCard<MockCharacterChoiceForBossCard>(Owner);
        var secondCard = Owner.RunState.CreateCard<MockCharacterChoiceForBossSecondCard>(Owner);
        var thirdCard = Owner.RunState.CreateCard<MockCharacterChoiceForBossThirdCard>(Owner);
        firstCard.MockedPool = firstCharacter.CardPool;
        secondCard.MockedPool = secondCharacter.CardPool;
        thirdCard.MockedPool = thirdCharacter.CardPool;

        ((StringVar)firstCard.DynamicVars["Characters"]).StringValue = firstCharacter.Title.GetFormattedText();
        ((StringVar)secondCard.DynamicVars["Characters"]).StringValue = secondCharacter.Title.GetFormattedText();
        ((StringVar)thirdCard.DynamicVars["Characters"]).StringValue = thirdCharacter.Title.GetFormattedText();

        var card = await CardSelectCmd.FromChooseACardScreen(new BlockingPlayerChoiceContext(), [firstCard, secondCard, thirdCard], Owner, canSkip: false);

        if (card == firstCard)
        {
            CharacterId = firstCharacter.Id;
        }
        else if (card == secondCard)
        {
            CharacterId = secondCharacter.Id;
        }
        else
        {
            CharacterId = thirdCharacter.Id;
        }

        var list = new List<Reward>();
        for (int i = 0; i < DynamicVars["UncommonCount"].IntValue; i++)
        {
            var options = CardCreationOptions.ForNonCombatWithUniformOdds([Character!.CardPool], c => c.Rarity == CardRarity.Uncommon).WithFlags(CardCreationFlags.NoRarityModification).WithFlags(CardCreationFlags.NoUpgradeRoll);
            list.Add(new UpgradedCardReward(options, 3, Owner));
        }
        for (int i = 0; i < DynamicVars["RareCount"].IntValue; i++)
        {
            var options = CardCreationOptions.ForNonCombatWithUniformOdds([Character!.CardPool], c => c.Rarity == CardRarity.Rare).WithFlags(CardCreationFlags.NoRarityModification);
            list.Add(new CardReward(options, 3, Owner));
        }
        await RewardsCmd.OfferCustom(Owner, list);
    }
    public override bool TryModifyRewards(Player player, List<Reward> rewards, AbstractRoom? room)
    {
        if (player != Owner || room == null)
        {
            return false;
        }

        CardRarityOddsType oddsType;
        switch(room.RoomType)
        {
            case RoomType.Monster:
                oddsType = CardRarityOddsType.RegularEncounter;
                break;
            case RoomType.Elite:
                oddsType = CardRarityOddsType.EliteEncounter;
                break;
            case RoomType.Boss:
                oddsType = CardRarityOddsType.BossEncounter;
                break;
            default:
                return false;
        }

        var option = new CardCreationOptions([Character!.CardPool], CardCreationSource.Encounter, oddsType).WithFlags(CardCreationFlags.NoCardPoolModifications);

        rewards.Add(new CardReward(option, 3, Owner));
        return true;
    }

}
