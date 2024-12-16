using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace GameControllers.Player.Interface
{
    public class EnergyBarUpdater : MonoBehaviourPun
    {
        [SerializeField] private Image _energyBar;

        private void OnEnable()
        {
            EnergyHandler.OnUpdateEnergyBar += UpdateBar;
        }

        private void OnDisable()
        {
            EnergyHandler.OnUpdateEnergyBar -= UpdateBar;
        }

        private void UpdateBar(float energyValue)
        {
            if (!photonView.IsMine) return;

            _energyBar.fillAmount = energyValue;
            photonView.RPC("SendProgressEnergyBar", RpcTarget.Others, energyValue);
        }

        [PunRPC]
        private void SendProgressEnergyBar(float energyValue)
        {
            _energyBar.fillAmount = energyValue;
        }
    }
}

