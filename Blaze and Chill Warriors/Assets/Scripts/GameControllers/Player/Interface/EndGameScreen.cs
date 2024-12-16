using System.Collections.Generic;
using MainMenuControllers.Store;
using Photon.Pun;
using PlayersData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameControllers.Player.Interface
{
    public class EndGameScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _endGameScreen;
        [SerializeField] private TMP_Text _ratePlayer1Text;
        [SerializeField] private TMP_Text _ratePlayer2Text;
        [SerializeField] private Image _player1Icon;
        [SerializeField] private Image _player2Icon;
        [SerializeField] private AudioSource _gameEndSound;

        private void OnEnable()
        {
            ScoreUpdater.OnEndGame += EndGame;
        }

        private void OnDisable()
        {
            ScoreUpdater.OnEndGame -= EndGame;
        }

        private void EndGame(PhotonView photonView)
        {
            _gameEndSound.Play();
            
            var myActorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
            var isFirstPlayer = true;

            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                if (myActorNumber < player.ActorNumber)
                    isFirstPlayer = false;
            }

            var currentRating = PlayerPrefs.GetInt(PlayerDataKeys.PlayerRatingKey);

            if (isFirstPlayer)
            {   
                var indexCharacterData = PlayerPrefs.GetInt(CharactersDataKeys.IndexChosenRedWolfKey);

                UpdatePlayerStats(
                    _ratePlayer1Text,
                    _player1Icon,
                    currentRating, 
                    indexCharacterData,
                    WolfsDataContainer.RedWolfsData);

                photonView.RPC("SyncFirstPlayerStats", RpcTarget.All, currentRating, indexCharacterData);
            }
            else
            {
                var indexCharacterData = PlayerPrefs.GetInt(CharactersDataKeys.IndexChosenBlueWolfKey);

                UpdatePlayerStats(
                    _ratePlayer2Text,
                    _player2Icon,
                    currentRating, 
                    indexCharacterData,
                    WolfsDataContainer.BlueWolfsData);

                photonView.RPC("SyncSecondPlayerStats", RpcTarget.All, currentRating, indexCharacterData);
            }

            _endGameScreen.SetActive(true);
        }

        private void UpdatePlayerStats<T>(
            TMP_Text playerRateText,
            Image playerIcon,
            int currentRating, 
            int indexCharacterData,
            List<T> wolfsData)
            where T : CharacterData
        {
            playerRateText.text = $"Rate: {currentRating}";
            playerIcon.sprite = wolfsData[indexCharacterData].CharacterIcon;
        }

        [PunRPC]
        private void SyncFirstPlayerStats(int currentRating, int indexCharacterData)
        {
            UpdatePlayerStats(
                _ratePlayer1Text,
                _player1Icon,
                currentRating, 
                indexCharacterData,
                WolfsDataContainer.RedWolfsData);
        }

        [PunRPC]
        private void SyncSecondPlayerStats(int currentRating, int indexCharacterData)
        {
            UpdatePlayerStats(
                _ratePlayer2Text,
                _player2Icon,
                currentRating, 
                indexCharacterData,
                WolfsDataContainer.BlueWolfsData);
        }
    }
}

