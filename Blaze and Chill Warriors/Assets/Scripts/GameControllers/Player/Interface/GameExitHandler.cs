using Photon.Pun;

namespace GameControllers.Player.Interface
{
    public class GameExitHandler : MonoBehaviourPun
    {
        public void ClickLeave()
        {
            photonView.RPC("ExitGame", RpcTarget.All);
        }

        [PunRPC]
        private void ExitGame()
        {
            PhotonNetwork.LeaveRoom();
        }
    }
}

