using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenuControllers
{
    public class MatchmakingHandler : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TMP_Text _countPlayersText;
        [SerializeField] private TMP_Text _searchButtonText;
        [SerializeField] private Button _searchButton;
        private Coroutine _animationLoadingText;
        private const int maxPlayers = 2;

        private void Update()
        {
            UpdatePlayerCount();
        }

        public void ClickStartMatchmaking()
        {
            PhotonNetwork.JoinRandomRoom();
            _animationLoadingText = StartCoroutine(StartLoadingTextAnimation());
            _searchButton.interactable = false;
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            var roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = maxPlayers; 

            PhotonNetwork.CreateRoom(null, roomOptions); 
        }

        public override void OnJoinedRoom()
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == maxPlayers)
                LoadGame();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == maxPlayers)
                LoadGame();
        }

        private void LoadGame()
        {
            if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.LoadLevel("Game");
        }

        private void UpdatePlayerCount()
        {
            var playerCount = PhotonNetwork.CountOfPlayers;
            _countPlayersText.text = "Players Online: " + playerCount;
        }

        private IEnumerator StartLoadingTextAnimation()
        {
            while (true)
            {
                _searchButtonText.text = "Finding\nmatch";
                yield return new WaitForSeconds(0.2f);

                _searchButtonText.text = "Finding\nmatch.";
                yield return new WaitForSeconds(0.2f);

                _searchButtonText.text = "Finding\nmatch..";
                yield return new WaitForSeconds(0.2f);

                _searchButtonText.text = "Finding\nmatch...";
                yield return new WaitForSeconds(0.2f);
            }
        } 
    }
}

