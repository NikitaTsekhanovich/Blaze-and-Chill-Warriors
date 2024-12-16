using System;
using System.Collections;
using GameControllers.GameEntites.Balls;
using Photon.Pun;
using UnityEngine;

namespace GameControllers.Player
{
    public class StunHandler : MonoBehaviourPun
    {
        [SerializeField] private AudioSource _stunSound;
        [SerializeField] private PlatformMovement _platformMovement;
        [SerializeField] private ButtonPressedController _buttonPressedController;
        [SerializeField] private AnimationsController _animationsController;

        private void OnEnable()
        {
            StunBall.OnStanPlayer += BeStunned;
        }

        private void OnDisable()
        {
            StunBall.OnStanPlayer -= BeStunned;
        }

        public void BeStunned(float stunTime)
        {
            if (!photonView.IsMine) return;

            _platformMovement.ChangeStateMove(false);
            _buttonPressedController.ChangeStateButton(false);
            _animationsController.StartStunAnimation(false);

            _stunSound.Play();
            StartCoroutine(WaitStunTime(stunTime));
        }

        private IEnumerator WaitStunTime(float stunTime)
        {
            yield return new WaitForSeconds(stunTime);
            _platformMovement.ChangeStateMove(true);
            _buttonPressedController.ChangeStateButton(true);
            _animationsController.StartStunAnimation(true);
        }
    }
}
