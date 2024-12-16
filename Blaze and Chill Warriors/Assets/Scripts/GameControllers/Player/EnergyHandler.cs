using System;
using System.Collections;
using Photon.Pun;
using UnityEngine;

namespace GameControllers.Player
{
    public class EnergyHandler : MonoBehaviourPun
    {
        [SerializeField] private ShootSystem _shootSystem;
        private float _currentEnergyValue;
        private Coroutine _accumulatedCoroutine;
        private const float maxEnergyValue = 3f;
        private const float delayReplenishment = 0.01f;
        private const float speedChangeEnergy = 120f;

        public static Action<float> OnUpdateEnergyBar;

        private void OnEnable()
        {
            ButtonPressedController.OnStartAccumulatingEnergy += StartAccumulatingEnergy;
            ButtonPressedController.OnStopAccumulatingEnergy += StopAccumulatingEnergy;
        }

        private void OnDisable()
        {
            ButtonPressedController.OnStartAccumulatingEnergy -= StartAccumulatingEnergy;
            ButtonPressedController.OnStopAccumulatingEnergy -= StopAccumulatingEnergy;
        }

        private IEnumerator AccumulateEnergy()
        {
            while (true)
            {
                yield return new WaitForSeconds(delayReplenishment);

                if (_currentEnergyValue < maxEnergyValue)
                {
                    _currentEnergyValue += delayReplenishment * Time.fixedDeltaTime * speedChangeEnergy;
                    OnUpdateEnergyBar.Invoke(_currentEnergyValue / maxEnergyValue);    
                }
            }
        }

        private void StartAccumulatingEnergy()
        {
            if (!photonView.IsMine) return;
            
            _accumulatedCoroutine = StartCoroutine(AccumulateEnergy());
        }

        private void StopAccumulatingEnergy()
        {
            if (!photonView.IsMine) return;

            StopCoroutine(_accumulatedCoroutine);

            _shootSystem.Shoot(_currentEnergyValue, maxEnergyValue);

            _currentEnergyValue = 0;
            OnUpdateEnergyBar.Invoke(_currentEnergyValue);

        }
    }
}

