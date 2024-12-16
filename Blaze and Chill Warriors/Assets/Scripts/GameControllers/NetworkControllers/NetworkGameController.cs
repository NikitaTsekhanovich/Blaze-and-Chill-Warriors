using GameLoaders;
using Photon.Pun;

namespace GameControllers.NetworkGameControllers
{
    public class NetworkGameController : MonoBehaviourPunCallbacks
    {
        public override void OnLeftRoom()
        {
            LoadingScreenController.Instance.ChangeScene("MainMenu");
        }
    }
}

