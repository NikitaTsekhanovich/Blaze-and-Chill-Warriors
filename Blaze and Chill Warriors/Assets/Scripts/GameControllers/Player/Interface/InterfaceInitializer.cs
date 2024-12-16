using Photon.Pun;
using UnityEngine;

namespace GameControllers.Player.Interface
{
    public class InterfaceInitializer : MonoBehaviourPun
    {
        [SerializeField] private GameObject _attackButton;

        public static PhotonView PhotonView { get; private set; }

        private void Start()
        {
            if (photonView.IsMine)
            {
                PhotonView = photonView;
                _attackButton.SetActive(true);
            }
        }
    }
}

