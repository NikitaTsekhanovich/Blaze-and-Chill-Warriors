using PlayersData;
using UnityEngine;

namespace MainMenuControllers.Store
{
    [CreateAssetMenu(fileName = "BlueWolfData", menuName = "Character data/ Blue Wolf Data")]
    public class BlueWolfData : CharacterData
    {
        public override TypeStateStoreItem TypeStateStoreItem 
        {
            get 
            {
                var indexOpenCharacter = PlayerPrefs.GetInt(CharactersDataKeys.IndexChosenBlueWolfKey);

                if (indexOpenCharacter == Index)
                    return TypeStateStoreItem.IsChosen;

                var playerBestRating = PlayerPrefs.GetInt(PlayerDataKeys.PlayerBestRatingKey);

                if (playerBestRating >= RatingToOpen)
                    return TypeStateStoreItem.IsOpen;

                return TypeStateStoreItem.IsLocked;
            } 
        }

        public override string IndexEntryKey => CharactersDataKeys.IndexChosenBlueWolfKey;
    }
}

