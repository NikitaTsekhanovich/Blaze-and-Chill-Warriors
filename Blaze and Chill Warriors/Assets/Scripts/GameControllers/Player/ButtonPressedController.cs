using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameControllers.Player
{
    public class ButtonPressedController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private AudioSource _downButtonSound;
        [SerializeField] private AudioSource _upButtonSound;
        [SerializeField] private Button _attackButton;
        private bool _notPlayerStun = true;
        private bool _isAlreadyPressed;

        public static Action OnStartAccumulatingEnergy;
        public static Action OnStopAccumulatingEnergy;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_notPlayerStun && !_isAlreadyPressed)
            {
                OnStartAccumulatingEnergy.Invoke();
                _isAlreadyPressed = true;
                _downButtonSound.Play();
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_notPlayerStun)
            {
                OnStopAccumulatingEnergy.Invoke();
                _isAlreadyPressed = false;
                _upButtonSound.Play();
            }
        }

        public void ChangeStateButton(bool isInteractable)
        {
            _notPlayerStun = isInteractable;
            _attackButton.interactable = isInteractable;
        }
    }
}
