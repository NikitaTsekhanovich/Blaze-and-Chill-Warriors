using PlayersData;
using TMPro;
using UnityEngine;

namespace MainMenuControllers
{
    public class PlayerInformationInitializer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _ratingPlayerText;

        private void Start()
        {
            LoadRating();
        }

        private void LoadRating()
        {
            _ratingPlayerText.text = $"Best Rating: {PlayerPrefs.GetInt(PlayerDataKeys.PlayerBestRatingKey)}";
        }
    }
}

