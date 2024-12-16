using System.Text;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace SinglePlayer
{
    public class SinglePlayerStartUp : MonoBehaviourPunCallbacks
    {
        private readonly StringBuilder _code = new();
        private int _counter;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _code.Append("Q");
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                _code.Append("W");
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                _code.Append("E");

                if (_code.ToString() == "QWE")
                {
                    StartSinglePlayer();
                }
                else
                {
                    _code.Clear();
                }
            }
        }

        public void ClickLogo()
        {
            _counter++;
            if (_counter % 10 == 0)
            {
                StartSinglePlayer();
            }
        }

        private void StartSinglePlayer()
        {
            PhotonNetwork.JoinRandomRoom();
        }
        
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            var roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 1; 

            PhotonNetwork.CreateRoom(null, roomOptions); 
        }
        
        public override void OnJoinedRoom()
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
                LoadGame();
        }
        
        private void LoadGame()
        {
            if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.LoadLevel("Game");
        }
    }
}
