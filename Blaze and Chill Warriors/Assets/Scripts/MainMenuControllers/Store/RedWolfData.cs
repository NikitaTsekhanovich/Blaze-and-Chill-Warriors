using PlayersData;
using UnityEngine;

namespace MainMenuControllers.Store
{
    [CreateAssetMenu(fileName = "RedWolfData", menuName = "Character data/ Red Wolf Data")]
    public class RedWolfData : CharacterData
    {
        public override TypeStateStoreItem TypeStateStoreItem 
        {
            get 
            {
                var indexOpenCharacter = PlayerPrefs.GetInt(CharactersDataKeys.IndexChosenRedWolfKey);

                if (indexOpenCharacter == Index)
                    return TypeStateStoreItem.IsChosen;

                var playerBestRating = PlayerPrefs.GetInt(PlayerDataKeys.PlayerBestRatingKey);

                if (playerBestRating >= RatingToOpen)
                    return TypeStateStoreItem.IsOpen;

                return TypeStateStoreItem.IsLocked;
            } 
        }

        public override string IndexEntryKey => CharactersDataKeys.IndexChosenRedWolfKey;
    }
}

